pipeline {
    agent any 
    stages {
        stage('Build') {
            steps {
                echo 'Restoring...'
                bat 'dotnet restore'
                echo 'Restored!'
                
                echo 'Building ....' 
                bat 'dotnet build'
                echo 'Build!'
            }
        }
    }
}
