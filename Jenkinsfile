pipeline {
    agent { 
        dockerfile true 
    }
    environment {
        NEW_VERSION = '1.1.0'
    }
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
        stage('testing-commands') {
            when {
                expression {
                    BRANCH_NAME == 'main' 
                }
            }
            steps {
                echo "Main branch with version ${NEW_VERSION}"
            }
        }   
    }
}
