# 🔐 CryptorAllProjects

This repository contains two related projects based on the .NET platform:

- `login/` – A WPF application for secure file encryption and decryption with integrated document viewing.
- `CryptorWinUIDLL/` – A helper library based on .NET MAUI used for Windows Hello authentication.

---

## 🧩 What Does Cryptor Do?

Cryptor is a utility for protecting confidential data that enables you to encrypt and decrypt files **without an internet connection**.

### Key Features:
- 🛡️ **Document encryption** using AES-256 + PBKDF2
- 📄 **Support for popular formats**: PDF, DOCX, XLSX, TXT
- 🔍 **Built-in document preview** without saving to disk (in-memory rendering)
- 🔐 **Windows Hello support** for biometric authentication
- 🌐 **Offline mode** — no cloud or internet required

---

## ⚠️ Current Code Status

The repository contains several **placeholder classes**, **experimental modules**, and **unimplemented ideas** that were created during the active development phase. These include:

- unfinished stubs and concepts;
- prototypes not yet integrated into the main application;
- unused views or services planned for future features.

These components are intentionally retained for development transparency and potential future use. They may be moved or separated into different branches in upcoming updates.

---

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
## 📚 Keywords (for discoverability)

AES file encryption, offline file security, C# encryption tool, WPF secure app, Windows Hello login, encrypted document viewer, PDF encryption, DOCX protection, .NET 8 privacy app, secure viewer, no-cloud encryption
