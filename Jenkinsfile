pipeline {
  agent { label "linux" }
  stages {
    stage("Build docker image") {
      steps {
        sh "docker build -t songapi ."
      }
    }
    stage("Run docker image") {
      steps {
        sh "docker run songapi"
      }
    }
  }
}