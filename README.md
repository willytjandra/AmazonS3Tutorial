# Developing with Amazon S3 bucket locally

## Introduction

In recent project, I have to upload files to Amazon (AWS) S3 bucket. It will be hard for us developer if we always have to connect to the cloud during our development, especially if we're offline. 🤷‍♂️

Luckily for AWS S3 bucket, there is [LocalStack](https://github.com/localstack/localstack) which spins up AWS Cloud API in our local machine.

> LocalStack supports various cloud API, not just S3. Checkout supported API [here](https://github.com/localstack/localstack#overview). With Pro license, you get more API supports.

So, I created this repo to remind my future self that there ia way to run AWS Cloud API in my local machine.

## Contents
- [Developing with Amazon S3 bucket locally](#developing-with-amazon-s3-bucket-locally)
    - [Introduction](#introduction)
    - [Contents](#contents)
    - [Sample Code](#sample-code)
        - [Getting Started](#getting-started)
    - [Summary](#summary)

## Sample Code

This repo contains a minimal sample code to upload a file into S3 bucket in LocalStack. It is a console app which will upload `hello-world.txt` to S3 bucket in LocalStack.

### Getting Started

First, let's start LocalStack in our local development machine. 
The easiest way to get LocalStack up and running in your local machine is to use [docker](https://www.docker.com/get-started).

I have provided a [`docker-compose.yml`](./docker-compose.yml) file which will run LocalStack in a container.

Open your terminal window and run:
```
docker-compose up
```

You can verify if LocalStack is up and running by navigating to [`http://localhost:4566/`](http://localhost:4566/). 

```json
{"status": "running"}
```

You can also try the health check to ensure the services required (in this case S3) is running: [`http://localhost:4566/health`](http://localhost:4566/health). 

```json
{"services": {"s3": "running"}}
```

Next, create an S3 bucket in your LocalStack.

```
aws --endpoint-url=http://localhost:4566 s3 mb s3://hello-s3
```
> This command will create a bucket named `hello-s3`.

Then, configure the bucket name in [`appsettings.Development.json`](./src/AmazonS3Tutorial.ConsoleApp/appsettings.Development.json).

Finally, let's build and run our sample code. The sample code will upload [`hello-world.txt`](./src/AmazonS3Tutorial.ConsoleApp/hello-world.txt) to the local S3 bucket named `hello-s3`.

Once the sample code runs successfully, run below command to verify that `hello-world.txt` is uploaded successfully.

```
aws --endpoint-url=http://localhost:4566 s3 ls s3://hello-s3
```

<img src="./docs/2021-05-10 21_23_31-localstack-s3-list-files.png" />

You can use browser to check the file: [http://localhost:4566/hello-s3/20210510_092321_hello-world.txt](http://localhost:4566/hello-s3/20210510_092321_hello-world.txt).

> The sample code prefix the file with timestamp. Please update above url accordingly.

## Summary

There you go!!! 🎉🎉🎉

With LocalStack, you can now have AWS Cloud API running in your local development machine.

Cya... 👋

