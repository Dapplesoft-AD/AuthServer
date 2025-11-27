# 🚀 AuthServer CI/CD Workflows

## Overview

This directory contains GitHub Actions workflows for building, testing, and deploying the AuthServer application.

---

## 📋 Workflows

| Workflow | File | Trigger | Purpose |
|----------|------|---------|---------|
| **Build** | `build.yml` | Push to `main`/`Dev` | Build, restore & test |
| **CI/CD** | `ci-cd.yml` | Push to `main`/`Dev` | Build & push Docker image to GHCR |

---

## 🔧 build.yml

**Purpose:** Validates code compiles and tests pass.

```yaml
Triggers:
  - Push to main/Dev branches
  - Manual dispatch (workflow_dispatch)

Steps:
  1. Checkout code
  2. Setup .NET 9.x
  3. Restore packages
  4. Build solution
  5. Run tests
```

---

## 🐳 ci-cd.yml

**Purpose:** Builds and pushes Docker image to GitHub Container Registry (GHCR).

```yaml
Triggers:
  - Push to main/Dev branches

Steps:
  1. Checkout code
  2. Login to GHCR
  3. Build Docker image
  4. Push to ghcr.io/dapplesoft-ad/auth-server:latest
```

### Registry Details

| Setting | Value |
|---------|-------|
| Registry | `ghcr.io` |
| Image | `ghcr.io/dapplesoft-ad/auth-server` |
| Tag | `latest` |

### Required Permissions

```yaml
permissions:
  packages: write  # Push to GHCR
  contents: read   # Checkout code
```

---

## 🐳 Docker Build Optimization

### Multi-Stage Build Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    MULTI-STAGE BUILD                        │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│   Stage 1: BASE        aspnet:9.0-alpine     (~110 MB)     │
│   Stage 2: BUILD       sdk:9.0-alpine        (~500 MB)     │
│   Stage 3: PUBLISH     sdk:9.0-alpine        (artifacts)   │
│   Stage 4: FINAL       aspnet:9.0-alpine     (~134 MB) ✅  │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

### Size Comparison

| Configuration | Final Image Size |
|---------------|------------------|
| ❌ Single stage (SDK) | ~900 MB |
| ⚠️ Multi-stage (Debian) | ~220 MB |
| ✅ **Multi-stage (Alpine)** | **~134 MB** |

### Optimizations Applied

| Optimization | Description | Savings |
|--------------|-------------|---------|
| **Multi-stage build** | SDK excluded from final image | ~700 MB |
| **Alpine Linux** | Smaller base image | ~90 MB |
| **Layer caching** | .csproj copied first for restore cache | Build time |
| **UseAppHost=false** | No native executable | ~10 MB |
| **.dockerignore** | Excludes bin/, obj/, .git/ | Context size |

### Dockerfile Location

```
src/Web.Api/Dockerfile
```

---

## 🔐 Required Secrets

| Secret | Description | Required By |
|--------|-------------|-------------|
| `GITHUB_TOKEN` | Auto-provided by GitHub | ci-cd.yml |

### Optional (for deployment)

| Secret | Description |
|--------|-------------|
| `SERVER_IP` | Deployment server IP |
| `SERVER_USER` | SSH username |
| `SERVER_SSH_KEY` | SSH private key |
| `SERVER_GHCR_PAT` | PAT for pulling images on server |

---

## 📦 Pulling the Image

```bash
# Login to GHCR (if private)
echo $GHCR_PAT | docker login ghcr.io -u USERNAME --password-stdin

# Pull the image
docker pull ghcr.io/dapplesoft-ad/auth-server:latest

# Run the container
docker run -d -p 8080:8080 ghcr.io/dapplesoft-ad/auth-server:latest
```

---

## 📊 Build Metrics

| Metric | Value |
|--------|-------|
| .NET Version | 9.0 |
| Base Image | `aspnet:9.0-alpine` |
| Final Image Size | ~134 MB |
| Build Time | ~2 minutes |
| Exposed Ports | 8080, 8081 |

---

## 🔄 Workflow Diagram

```
┌──────────────┐     ┌──────────────┐     ┌──────────────┐
│   Developer  │────▶│    GitHub    │────▶│    GHCR      │
│   Push Code  │     │   Actions    │     │   Registry   │
└──────────────┘     └──────────────┘     └──────────────┘
                            │
                            ▼
                     ┌──────────────┐
                     │  build.yml   │
                     │  - Restore   │
                     │  - Build     │
                     │  - Test      │
                     └──────────────┘
                            │
                            ▼
                     ┌──────────────┐
                     │  ci-cd.yml   │
                     │  - Docker    │
                     │    Build     │
                     │  - Push to   │
                     │    GHCR      │
                     └──────────────┘
```

---

## 📝 Notes

- Workflows run on `ubuntu-latest`
- Docker images use Alpine Linux for minimal size
- GITHUB_TOKEN is automatically provided by GitHub Actions
- Images are tagged with `latest` (consider adding SHA tags for production)

