﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeeNetServer.Resources {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BeeNetServer.Resources.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 正在添加图片{0}。 的本地化字符串。
        /// </summary>
        internal static string AddingPicture {
            get {
                return ResourceManager.GetString("AddingPicture", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 文件不存在！ 的本地化字符串。
        /// </summary>
        internal static string FileNotExist {
            get {
                return ResourceManager.GetString("FileNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 非法操作！由于前一个操作未完成！ 的本地化字符串。
        /// </summary>
        internal static string IllegalOperateBecausePreviousNotFinish {
            get {
                return ResourceManager.GetString("IllegalOperateBecausePreviousNotFinish", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 在队列中找不到指定图片，请刷新页面后重试！ 的本地化字符串。
        /// </summary>
        internal static string NotFindPictureInQueue {
            get {
                return ResourceManager.GetString("NotFindPictureInQueue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 操作完成。 的本地化字符串。
        /// </summary>
        internal static string OperateFinish {
            get {
                return ResourceManager.GetString("OperateFinish", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 准备执行。 的本地化字符串。
        /// </summary>
        internal static string ReadyToRun {
            get {
                return ResourceManager.GetString("ReadyToRun", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 库中已经存在相同图片。 的本地化字符串。
        /// </summary>
        internal static string SamePictureInGallery {
            get {
                return ResourceManager.GetString("SamePictureInGallery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 添加队列中已经存在相同图片。 的本地化字符串。
        /// </summary>
        internal static string SamePictureInQueue {
            get {
                return ResourceManager.GetString("SamePictureInQueue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 正在保存到数据库中…… 的本地化字符串。
        /// </summary>
        internal static string SavingDatabaseChange {
            get {
                return ResourceManager.GetString("SavingDatabaseChange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 已经存在相似图片。 的本地化字符串。
        /// </summary>
        internal static string SimilarPicture {
            get {
                return ResourceManager.GetString("SimilarPicture", resourceCulture);
            }
        }
    }
}
