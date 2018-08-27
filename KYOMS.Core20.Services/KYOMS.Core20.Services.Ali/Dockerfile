# 声明使用的基础镜像
FROM microsoft/aspnetcore

# 将本地dotnet core 应用拷贝到 镜像 /app/ 目录下，COPY命令会自动创建缺失的目录
COPY publishoutput /app/

# 设置工作目录
WORKDIR /app/

# 设置导出端口
EXPOSE 15500/tcp

# 指定应用入口点
ENTRYPOINT ["./KYOMS.Core20.Services.Ali"]
