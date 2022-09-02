pipeline {

  agent { docker true }
  stages {
    stage("Get version") {
      steps {
        bat "dotnet --version"
      }
    }
  }
}
