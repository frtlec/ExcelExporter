pipeline {
  agent any
  environment {
    DISABLE_AUTH = 'true'
    DB_ENGINE    = 'sqlite'
    DOTNET_CLI_HOME = "/tmp/DOTNET_CLI_HOME"
  }
  stages {
    stage("verify tooling") {
      steps {
        sh '''
          docker version
          docker info
          docker-compose version
        '''
      }
    }
    // stage('Prune Docker data') {
    //   steps {
    //     sh 'docker system prune -a --volumes -f'
    //   }
    // }
    stage('Start container') {
      steps {
        sh 'sudo docker-compose down'
        sh 'sudo docker-compose build'
        sh 'sudo docker-compose up -d'
        sh 'sudo docker-compose ps'
      }
    }
  }
}