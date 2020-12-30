<#
.SYNOPSIS
Starts the required infrastructure using docker compose.

.DESCRIPTION
A docker container will be started for:
* Postgres
* Pg-admin
* Prometheus
* Grafana

.PARAMETER BaseFolder
Base folder where docker data will be persisted.

.EXAMPLE
.\start-infra.ps1 -BaseFolder "c:\docker"

.NOTES
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$BaseFolder
)


Write-Host "Copying configuration files for Prometheus"
New-Item "$BaseFolder/prometheus/config" -Force -ItemType Directory *> $null
Copy-Item "setup/prometheus/prometheus.yml" "$BaseFolder/prometheus/config"

Write-Host "Copying configuration files for Grafana"
New-Item "$BaseFolder/grafana/provisioning/dashboards" -Force -ItemType Directory *> $null
Copy-Item "setup/grafana/monitoring-dashboard-config.yml" "$BaseFolder/grafana/provisioning/dashboards"
Copy-Item "setup/grafana/dashboards/app-metrics-web-monitoring-prometheus_rev3.json" "$BaseFolder/grafana/provisioning/dashboards"
New-Item "$BaseFolder/grafana/provisioning/datasources" -Force -ItemType Directory *> $null
Copy-Item "setup/grafana/prometheus-datasource.yml" "$BaseFolder/grafana/provisioning/datasources"

Write-Host "Creating the volumes"
# https://cravencode.com/post/docker/create-named-docker-bind-mount/
docker volume create --driver local -o o=bind -o type=none -o device="$BaseFolder/prometheus/config" prometheus-config
docker volume create --driver local -o o=bind -o type=none -o device="$BaseFolder/prometheus/data" prometheus-data
docker volume create --driver local -o o=bind -o type=none -o device="$BaseFolder/grafana/data" grafana-data
docker volume create --driver local -o o=bind -o type=none -o device="$BaseFolder/grafana/provisioning" grafana-provisioning
docker volume create --driver local -o o=bind -o type=none -o device="$BaseFolder/elasticsearch/data" elasticsearch-data

Write-Host "Starting containers"
docker-compose up -d