pipeline {

  agent any
  stages {
    stage("Build docker image") {
      steps {
        bat "docker build -t songapi ."
      }
    }
    stage("Run docker image") {
      steps {
        bat "docker run songapi"
      }
    }
  }
}
