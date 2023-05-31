docker run -d -p 8001:80 -p 8002:443 --name lesson-registration-back --network lesson-registration --ip 172.20.0.3 ^
-e ASPNETCORE_URLS="https://+;http://+" ^
-e ASPNETCORE_HTTPS_PORT=8002 ^
-e ASPNETCORE_ENVIRONMENT=Development ^
-e ASPNETCORE_Kestrel__Certificates__Development__Password="1985" ^
lesson-registration-back