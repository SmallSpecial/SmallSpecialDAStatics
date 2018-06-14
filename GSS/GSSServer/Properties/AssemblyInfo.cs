﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的常规信息通过下列属性集
// 控制。更改这些属性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("GSS服务端")]
[assembly: AssemblyDescription("说明:GSS游戏服务系统(游戏运营平台)")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("北京神龙游科技有限公司")]
[assembly: AssemblyProduct("GSSServer游戏运营平台")]
[assembly: AssemblyCopyright("Copyright © 北京神龙游 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 属性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("adb5629d-39e0-43b9-b943-8c0f90d9b6bd")]

// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本 
//      内部版本号
//      修订号
//
// 可以指定所有这些值，也可以使用“内部版本号”和“修订号”的默认值，
// 方法是按如下所示使用“*”:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.8.53")]
[assembly: AssemblyFileVersion("1.0.8.53")]
//日志记录
[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"GSSData\GSSLog.gss", Watch = true)]