pipeline {
    agent { dockerfile true }
    stages {
        stage('Test') {
            steps {
                bat '''
                dotnet --version
                '''
            }
        }
    }
}