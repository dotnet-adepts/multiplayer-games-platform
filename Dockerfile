FROM microsoft/aspnetcore-build:2.0.0 AS build

WORKDIR /code

COPY ./GameApplication .

RUN dotnet restore

RUN dotnet publish --output /output --configuration Release


FROM microsoft/aspnetcore:2.0.0

COPY --from=build /output /app

WORKDIR /app

ARG fid

ARG fs

ARG gid

ARG gs

ENV FACEBOOK_ID=$fid

ENV FACEBOOK_SECRET=$fs

ENV GOOGLE_ID=$gid

ENV GOOGLE_SECRET=$gs

CMD ["dotnet", "GameApplication.dll"]