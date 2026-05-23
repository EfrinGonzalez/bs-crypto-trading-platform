# Deployment
- Dockerized services with environment-specific configs.
- Kubernetes base manifests and prod overlay in `deploy/k8s`.
- Helm-style value segregation pattern documented for migration.
- Azure-ready primitives: AKS, PostgreSQL Flexible Server, Key Vault, Monitor.
- CI/CD with GitHub Actions build/test/container-scan/deploy stages.
