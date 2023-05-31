FROM mcr.microsoft.com/dotnet/sdk:7.0 as BuildBack
COPY ./ /src
WORKDIR /src/
RUN dotnet publish -o /app
FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
EXPOSE 443
COPY --from=BuildBack /app /app
COPY ./ContainerVariables.json /app/variables.json
COPY ./LessonRegistration/cert.crt /app/cert.crt
COPY ./LessonRegistration.pfx /root/.aspnet/https/LessonRegistration.pfx
WORKDIR /app
CMD dotnet /app/LessonRegistration.dll