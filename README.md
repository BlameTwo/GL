# GL
> 使用WinUI3开发！

### 此分支为WinUI3分支，采用更流畅的设计，更加现代的UI，加上无缝的UI动画，呈现更好的效果。

## 基于WPF版的新特性
- 原生的云母和亚克力效果切换
- 采用纯页面导航，不采用缓存残留的方式，使运行时的内存降低到70-150mb左右。

## 修改
- 由于WinUI3不支持系统代理，服务器页面修改为单独运行，
- 下述为服务器补丁命令行
``` Powershell
start ProxyHelper.exe  #运行该补丁

## 预览
![](/Private/Home.png)
![](/Private/Notify.png)
![](/Private/Server.png)

exit    #为退出程序

start127.0.0.1  #start为连接代理，命令后跟一个IP地址，暂时不可修改端口

stop   #为结束代理

setup   #为安装证书

unsetup #为卸载证书

```

### 为Win11设计！

解锁帧率：[DGP.Genshin.FPSunlocking](https://github.com/DGP-Studio/DGP.Genshin.FPSUnlocking)
