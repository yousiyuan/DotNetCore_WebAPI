# 声明使用的基础镜像
FROM microsoft/aspnetcore:2.0

# 将本地dotnet core 应用拷贝到 镜像 /app/kyoms.core20.services.taobao/ 目录下，COPY命令会自动创建缺失的目录
COPY publish /app/

# 设置工作目录
WORKDIR /app/

# 设置导出端口
EXPOSE 15600/tcp

# 指定应用入口点
ENTRYPOINT ["./KYOMS.Services.Core20.TAOBAO"]


#dotnet publish -r centos.7-x64 -c release
#docker build -t kyoms.core20.services.taobao:1.0 .
#docker run -it --name kyoms.core20.services.taobao -p 15600:80  -v /root/app/log:/root/app/log --privileged=true  --restart always  -d 172.16.41.60/oms/kyoms.core20.services.taobao:1.1