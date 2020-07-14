﻿using BeeNetServer.CException;
using BeeNetServer.Data;
using BeeNetServer.Models;
using BeeNetServer.Parameters.Picture;
using BeeNetServer.Resources;
using BeeNetServer.Tool;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BeeNetServer.Background
{
    /// <summary>
    /// 添加图片线程类
    /// </summary>
    public static class PicturesAddProgress
    {
        /// <summary>
        /// 待添加的图片列表
        /// </summary>
        public static PicturePostParameters PicturePostParameters { get; set; }
        /// <summary>
        /// 进度指示器
        /// </summary>
        public static AddPicturesProgressIndicator TaskProgress { get; set; }
        /// <summary>
        /// 工作任务
        /// </summary>
        private static Task _workTask = null;

        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="pictures"></param>
        public static void CreateTask(List<Picture> pictures)
        {
            TaskProgress = new AddPicturesProgressIndicator();
            TaskProgress.SetProgress(0, "准备执行。");
            
            _workTask = new Task(Run, TaskCreationOptions.LongRunning);
            _workTask.Start();
        }

        public static void SaveImage()
        {
            foreach(var formFile in  PicturePostParameters.ImageFiles)
            {
                using (var stream = formFile.OpenReadStream())
                {
                    using var buffered= new BufferedStream(stream);
                    HashUtil.GetMD5(buffered)
                }
                    
            }
        }

        /// <summary>
        /// 运行
        /// </summary>
        public static void Run()
        {
            // 从本地添加
            using var scope = Program.IHost.Services.CreateScope();
            var services = scope.ServiceProvider;
            using var context = scope.ServiceProvider.GetRequiredService<BeeNetContext>();

            Dictionary<string, Picture> md5Dict = new Dictionary<string, Picture>();
            for (int idx = 0; idx < PictureExtensions.Count; idx++)
            {
                var pictureExtension = PictureExtensions[idx];
                var picture = pictureExtension.Picture;
                TaskProgress.SetProgress((float)idx / PictureExtensions.Count, string.Format(Resource.AddingPicture, Path.GetFileName(picture.Path)));
                HashUtil.ComplementPicture(picture);
                if (File.Exists(picture.Path))
                {
                    //var md5 = picture.MD5 = HashUtil.GetMD5ByHashAlgorithm(picture.Path);
                    var md5 = picture.MD5;
                    // 队列中是否已经包含相同图片
                    if (!md5Dict.ContainsKey(md5))
                    {
                        var conflictPicture = context.Pictures.FirstOrDefault(p => p.MD5 == md5);
                        if (conflictPicture == null)
                        {
                            // 启动相似性判断
                            if (UserSettingReader.UserSettings.PictureSettings.UseSimilarJudge)
                            {
                                //picture.PriHash = HashUtil.PriHash(picture.Path);
                                var conflictPictures = new List<Picture>();
                                var resPictures = context.Pictures;
                                foreach (var resPicture in resPictures)
                                {
                                    if (HashUtil.IsSimilar(picture.PriHash, resPicture.PriHash))
                                    {
                                        conflictPictures.Add(resPicture);
                                    }
                                }
                                foreach (var resPicture in PictureExtensions)
                                {
                                    if (resPicture.Picture != picture)
                                    {
                                        if (HashUtil.IsSimilar(picture.PriHash, resPicture.Picture.PriHash))
                                        {
                                            conflictPictures.Add(resPicture.Picture);
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (conflictPictures.Count == 0)
                                {
                                    // 通过判断
                                    md5Dict.Add(md5, picture);
                                    AddPicture(pictureExtension);
                                    context.Pictures.Add(picture);

                                }
                                else
                                {
                                    pictureExtension.SetError(Resource.SimilarPicture, conflictPictures);
                                }
                            }
                            else
                            {
                                picture.PriHash = null;
                                // 通过判断
                                md5Dict.Add(md5, picture);
                                AddPicture(pictureExtension);
                                context.Pictures.Add(picture);
                            }

                        }
                        else
                        {
                            pictureExtension.SetError(Resource.SamePictureInGallery, new List<Picture> { conflictPicture });
                        }

                    }
                    else
                    {
                        pictureExtension.SetError(Resource.SamePictureInQueue, new List<Picture> { md5Dict[md5] });
                    }
                }
                else
                {
                    pictureExtension.SetError(Resource.FileNotExist);
                }

            }
            TaskProgress.SetProgress(1.0f, Resource.SavingDatabaseChange);
            context.SaveChanges();
            TaskProgress.SetProgress(1.0f, Resource.OperateFinish);
            TaskProgress.TaskProgressStatus = AddPicturesProgressStatus.Finished;
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="pictureExtension"></param>
        /// <param name="md5"></param>
        private static void AddPicture(PictureExtension pictureExtension)
        {
            pictureExtension.Succeed();
            var picture = pictureExtension.Picture;
            var newFileName = picture.MD5 + Path.GetExtension(picture.Path);
            var newFilePath = Path.Combine(UserSettingReader.UserSettings.PictureSettings.PictureStorePath, newFileName);
            File.Copy(picture.Path, newFilePath, true);
            picture.Path = newFilePath;
        }

        public static async Task<Picture> ForceAddPicture(Picture picture)
        {
            if (TaskProgress.TaskProgressStatus == AddPicturesProgressStatus.Finished)
            {
                var res = PictureExtensions.SingleOrDefault(p => p.Picture.Path == picture.Path);
                if (res != null)
                {
                    using var scope = Program.IHost.Services.CreateScope();
                    var services = scope.ServiceProvider;
                    using var context = scope.ServiceProvider.GetRequiredService<BeeNetContext>();
                    context.Pictures.Add(res.Picture);
                    await context.SaveChangesAsync();
                    res.Succeed();
                    return res.Picture;
                }
                else
                {
                    throw new SimpleException(Resource.NotFindPictureInQueue);
                }
            }
            else
            {
                throw new SimpleException(Resource.IllegalOperateBecausePreviousNotFinish);
            }
        }

    }

}
