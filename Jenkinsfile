pipeline {

  agent { dockerfile true }
  stages {
    stage("Get version") {
      steps {
        bat "dotnet --version"
      }
    }
  }
}
