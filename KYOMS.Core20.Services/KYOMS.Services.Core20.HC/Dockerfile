# 声明使用的基础镜像
FROM microsoft/aspnetcore:2.0

# 将本地dotnet core 应用拷贝到 镜像 /app/kyoms.services.core20.hc/ 目录下，COPY命令会自动创建缺失的目录
COPY publishoutput /app/kyoms.services.core20.hc/

# 设置工作目录
WORKDIR  /app/kyoms.services.core20.hc/

# 设置导出端口
EXPOSE 15000/tcp

# 指定应用入口点
ENTRYPOINT ["./KYOMS.Services.Core20.HC"]


#发布项目指令
#dotnet publish -r centos.7-x64 -c release
#创建镜像
#docker build -t kyoms.services.core20.hc:1.0 .
#启动容器
#docker run -it --name hc -p 15000:80  -v /root/app/kyoms.services.core20.hc:/root/app --privileged=true  --restart always  -d kyoms.services.core20.hc:1.0