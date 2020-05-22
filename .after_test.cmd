nuget install OpenCover -OutputDirectory packages -Version 4.6.519
nuget install coveralls.net -OutputDirectory packages -Version 0.412.0


.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -target:dotnet.exe -targetargs:"test "".\MPT.Geometry.UnitTests\MPT.Geometry.UnitTests.csproj""" -filter:"+[MPT.Geometry*]*" -oldStyle -output:opencoverCoverage.xml

.\packages\coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i .\opencoverCoverage.xml