pipeline {
    agent any 
    stages {
        stage('Build/Publish') {
            steps {
                bat 'dotnet restore'
                bat 'dotnet publish --no-restore'
            }
        }
        stage('Unit tests') {
            steps {
                bat 'dotnet test --no-build --verbosity normal .\\Song.API.UnitTests\\'
            }
        }
        stage('Intergration tests') {
            steps {
                bat 'dotnet test --no-build --verbosity normal .\\Song.API.IntergrationTests\\'
            }
        }
    }
}
