FROM microsoft/aspnetcore-build:2.0.0

WORKDIR /code

COPY ./GameApplication .

RUN dotnet restore

RUN dotnet publish --output /output --configuration Release


FROM microsoft/aspnetcore:2.0.0

COPY --from=microsoft/aspnetcore-build:2.0.0 /output /app

WORKDIR /app

ENTRYPOINT ["dotnet", "DockerDemo.dll"]