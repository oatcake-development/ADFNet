# Makefile for building and packaging ADFNet as a NuGet package
SOLUTION_NAME := ADFNet
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
	@echo "📦 Packing NuGet package..."
	@mkdir -p $(NUGET_OUTPUT)
	dotnet pack $(SOLUTION_NAME).sln \
		-c $(CONFIGURATION) \
		-o $(NUGET_OUTPUT) \
		--include-symbols \
        --include-source


# Create NuGet package(s) (CI-optimised)
package-nobuild:
	@mkdir -p $(NUGET_OUTPUT)
	dotnet pack $(SOLUTION_NAME).sln -c $(CONFIGURATION) -o $(NUGET_OUTPUT) --no-build

# Create NuGet package(s) (CI-optimised Preview Nuget packaging)
package-preview-nobuild:
	@mkdir -p $(NUGET_OUTPUT)
	dotnet pack $(SOLUTION_NAME).sln -c $(CONFIGURATION) -o $(NUGET_OUTPUT) --no-build --version-suffix preview

# Clean build artifacts
clean:
	@echo "🧹 Cleaning build artifacts..."
	dotnet clean $(SOLUTION_NAME).sln -c $(CONFIGURATION)
	@rm -rf $(NUGET_OUTPUT)

