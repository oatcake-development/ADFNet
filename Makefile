# Makefile for building and packaging ADFNet as NuGet packages
SOLUTION_NAME := ADFNet
PROJECTS := ADFNet.Core ADFNet.Json ADFNet.OrgMode
NUGET_OUTPUT := ./nupkg
CONFIGURATION := Release

.PHONY: all build pack clean test test-nobuild package-nobuild package-preview-nobuild

# Default target
all: clean build test-nobuild package-nobuild

package-preview: clean build test-nobuild package-preview-nobuild

# Build the solution
build:
	@echo "🔧 Building $(SOLUTION_NAME)..."
	dotnet build $(SOLUTION_NAME).sln -c $(CONFIGURATION)

# Run unit tests (friendly)
test:
	@echo "🧪 Testing $(SOLUTION_NAME)..."
	dotnet test $(SOLUTION_NAME).sln -c $(CONFIGURATION)

# Run unit tests (CI-optimised)
test-nobuild:
	dotnet test $(SOLUTION_NAME).sln -c $(CONFIGURATION) --no-build

# Create NuGet package(s) (friendly)
pack:
	@echo "📦 Packing NuGet packages..."
	@mkdir -p $(NUGET_OUTPUT)
	@for proj in $(PROJECTS); do \
		echo "📦 Packing $$proj..."; \
		dotnet pack ./$$proj/$$proj.csproj \
			-c $(CONFIGURATION) \
			-o $(NUGET_OUTPUT)/$$proj \
			--include-symbols \
			--include-source; \
	done

# Create NuGet package(s) (CI-optimised)
package-nobuild:
	@mkdir -p $(NUGET_OUTPUT)
	@for proj in $(PROJECTS); do \
		echo "📦 Packing $$proj (no build)..."; \
		dotnet pack ./$$proj/$$proj.csproj \
			-c $(CONFIGURATION) \
			-o $(NUGET_OUTPUT)/$$proj \
			--no-build \
			--include-symbols \
			--include-source; \
	done

# Create NuGet package(s) (CI-optimised Preview Nuget packaging)
package-preview-nobuild:
	@mkdir -p $(NUGET_OUTPUT)
	@for proj in $(PROJECTS); do \
		echo "📦 Packing $$proj (preview, no build)..."; \
		dotnet pack ./$$proj/$$proj.csproj \
			-c $(CONFIGURATION) \
			-o $(NUGET_OUTPUT)/$$proj \
			--no-build \
			--version-suffix preview \
			--include-symbols \
			--include-source; \
	done

# Clean build artifacts
clean:
	@echo "🧹 Cleaning build artifacts..."
	dotnet clean $(SOLUTION_NAME).sln -c $(CONFIGURATION)
	@rm -rf $(NUGET_OUTPUT)

# For CI builds on push to main
ci-preview:
	$(MAKE) clean
	$(MAKE) build
	$(MAKE) test-nobuild
	$(MAKE) package-preview-nobuild

# For CI builds on tag
ci-release:
	$(MAKE) clean
	$(MAKE) build
	$(MAKE) test-nobuild
	$(MAKE) package-nobuild
