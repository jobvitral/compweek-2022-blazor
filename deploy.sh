#!/bin/bash

#inicia o processo
echo "Iniciando processo de deploy"
echo ""

#informa o projeto
echo "Projeto para deploy"
echo "1 - Identity Server"
echo "2 - API"
echo "3 - BlazorApp"
read -p "Informe o código do projeto para deploy [1]: " PROJECT_ID

PROJECT_ID=${PROJECT_ID:-1}

#carrega dados do projeto baseado no ID informado
case $PROJECT_ID in
    1)
        DOCKER_FILE="CompWeek.Identity.Dockerfile"
        CONTAINER_NAME="compweek-identity"
        CONTAINER_VERSION="1.0"
        CONTAINER_IP="10.88.0.5"
        CONTAINER_PORT="5000:80"
        ;;
    2)
        DOCKER_FILE="CompWeek.Api.Dockerfile"
        CONTAINER_NAME="compweek-api"
        CONTAINER_VERSION="1.0"
        CONTAINER_IP="10.88.0.3"
        CONTAINER_PORT="5001:80"
        ;;
    3)
        DOCKER_FILE="CompWeek.Web.Dockerfile"
        CONTAINER_NAME="compweek-web"
        CONTAINER_VERSION="1.0"
        CONTAINER_IP="10.88.0.4"
        CONTAINER_PORT="5002:80"
        ;;
esac

echo ""

echo "Detalhes do deploy"
echo "- Container: " $CONTAINER_NAME
echo "- Versao: " $CONTAINER_VERSION
echo "- IP: " $CONTAINER_IP
echo "- Porta: " $CONTAINER_PORT
echo "- Dockerfile: " $DOCKER_FILE
echo ""

echo "PASSO 3 - EFETUANDO DEPLOY"
echo "- Exclui a imagem atual"
sudo podman rmi -f $CONTAINER_NAME:$CONTAINER_VERSION

echo "- Build da imagem container"
sudo podman build --tag $CONTAINER_NAME:$CONTAINER_VERSION -f $DOCKER_FILE --network=host .

echo "- Exclui as imagens temporarias"
sudo podman images | grep none | awk '{ print $3; }' | xargs sudo podman rmi --force

echo "- Inicializa o container"
sudo podman run --name $CONTAINER_NAME --ip=$CONTAINER_IP --network=podman -p $CONTAINER_PORT -d $CONTAINER_NAME:$CONTAINER_VERSION