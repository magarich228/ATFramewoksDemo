docker compose -p at up -d

cd C:\Users\k.groshev\source\repos\rgb\Build\Debug\Testing

for /R C:\Users\k.groshev\source\repos\rgb\Bin %%f in (*.AutoTests.dll) do copy %%f ServerGRC.Web\SystemCatalog\CSC\Bin\

copy C:\Users\k.groshev\.nuget\packages\prometheus-net\8.2.0\lib\netstandard2.0\Prometheus.NetStandard.dll ServerGRC.Web\SystemCatalog\CSC\Bin\Prometheus.NetStandard.dll

ServerGRC.Web\CommonComponents.Catalog.IndexationUtility.exe

start /B ConfigurationAgent\UniversalPlatform.ConfigurationAgent.exe

ServerGRC.Web\ServerGRC.ConsoleHost.exe /debug