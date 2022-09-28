az login
az account list --output table
az account set --subscription “subscription id”
az account show --output  table

#Create resource group
$location="centralus"
$resourcegroup="demoResourceGroup"
$namespace="ajaydemoservice"
$sku="Standard"
az group create --name $resourcegroup --location $location

$queuename="ajayqueue"
$location="centralus"
$resourcegroup="demoResourceGroup"
$namespace="ajaydemoservice"

$sku="Standard"

#Create Namespace
az servicebus namespace create --resource-group $resourcegroup --name $namespace --location $location --sku $sku

#Create Queue
az servicebus queue create --resource-group $resourcegroup --namespace-name $namespace --name $queuename







