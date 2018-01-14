echo $1
sed -i 's_/woa/data/woa.db_'$1'_g' appsettings.Docker.json
cat appsettings.Docker.json
dotnet Woa.Backend.dll