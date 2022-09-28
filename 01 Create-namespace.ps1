az login -use-device-code
az account list --output table
az account set --subscription “subscription id”
az account show --output  table

#Create resource group
$location="centralus"
$resourcegroup="demoResourceGroup"
$namespace="ajaydemoservice"
$sku="Standard"

$queuename="ajayqueue"
$location="centralus"
$resourcegroup="demoResourceGroup"
$namespace="ajaydemoservice"

$sku="Standard"

#create resource group
az group create --name $resourcegroup --location $location

#Create Namespace
az servicebus namespace create --resource-group $resourcegroup --name $namespace --location $location --sku $sku

az servicebus namespace list --resource-group $resourcegroup --output table

#Create Queue
az servicebus queue create --resource-group $resourcegroup --namespace-name $namespace --name $queuename

#list the queues
az servicebus queue list --resource-group $resourcegroup --namespace-name $namespace --output table







