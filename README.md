# CryptorAllProjects

This repository contains two separate .NET-based projects:

- `login/` – A WPF application for file encryption and decryption, designed as a desktop interface.
- `CryptorWinUIDLL/` – A supporting library or MAUI-based component for authentication or cross-platform features.

## 🚀 How to Run

To run the main desktop application:

1. Open the solution file located in the `login` folder:  
   `login/CryptorLogin.sln`

2. Build and run the project using **Visual Studio 2022** (or later) with **.NET 8 SDK** installed.

3. Ensure all dependencies are restored via NuGet on first build.

## 🧩 Project Purpose

The WPF application provides a secure interface for encrypting and decrypting documents with support for multiple file types and themes.

The secondary MAUI or library project may be used to extend functionality such as authentication (e.g., Windows Hello) or shared logic across platforms.

## 📦 Structure

CryptorAllProjects/
├── login/ # WPF application
│ └── CryptorLogin.sln
└── CryptorWinUIDLL/ # MAUI / shared library component
└── CryptorWinUIDLL.sln
