# Makefile for building and packaging ADFNet as a NuGet package

PROJECT_NAME := ADFNet.Core
CONFIGURATION := Release
OUTPUT_DIR := ./nupkg
CS_PROJ := ./ADFNet.Core/ADFNet.Core.csproj

.PHONY: all build clean pack

all: build pack

build:
	@echo "🔧 Building $(PROJECT_NAME)..."
	dotnet build $(CS_PROJ) -c $(CONFIGURATION)

clean:
	@echo "🧹 Cleaning build artifacts..."
	dotnet clean $(CS_PROJ)
	rm -rf $(OUTPUT_DIR)

pack:
	@echo "📦 Packing NuGet package..."
	dotnet pack $(CS_PROJ) \
		-c $(CONFIGURATION) \
		-o $(OUTPUT_DIR) \
		--include-symbols \
		--include-source

