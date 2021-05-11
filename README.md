# Azure Function that receives data via HTTP Trigger (eg. from Solace RDP), and pushes it to blob storage

## Overview

This project will get you started with receiving HTTP requests in an Azure Function, and pushing the request body to blob storage.

## Prerequisites

Azure Blob Storage configured

## Setting Variables

The blob storage Account Name and Access Key need to be set in the BlobStorageFunction file. Those can be found in the Azure Portal: https://docs.microsoft.com/en-us/azure/storage/common/storage-account-keys-manage?tabs=azure-portal

## Deploy the Azure Function

To publish the Azure Function: https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs#publish-to-azure