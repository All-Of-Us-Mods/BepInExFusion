# AGENTS.md - Coding Guidelines for BepInEx Fusion

This document provides mandatory guidelines for all AI agents and developers contributing to the BepInEx Fusion project. Adherence to these principles is non-negotiable to ensure code quality, stability, and optimal performance on Android devices.

---

## ⚠️ 1. Core Technical Constraints (MANDATORY)

**The most critical point of failure in this repository is misunderstanding the runtime environment.** Any agent failing to adhere to these constraints will produce incompatible or unstable code.

### A. Runtime Environment
*   **CLR:** The entire ecosystem runs on **.NET 10 CoreCLR**.
*   **DO NOT USE MONO:** Never assume Mono compatibility, syntax, or behavior. Assume only modern, high-performance .NET standards.
*   **Platform Target:** Android devices. This implies resource constraints (limited CPU/RAM) and potential differences in threading models compared to desktop environments.

### B. Performance Focus (Android Optimization)
1.  **Minimize Allocations:** Excessive garbage collection (GC) cycles are detrimental on mobile hardware. Agents must prioritize methods that minimize object instantiation, especially inside tight loops or event handlers (`OnUpdate`, `Tick`, etc.). Favor pooling and reuse over constant creation.
2.  **Async Operations:** Use `async` and `await` correctly for I/O operations (file access, network calls) to prevent blocking the main thread (which often handles game rendering). Never block on an async call unnecessarily.
3.  **Startup Time:** Code must be highly optimized during startup. Excessive work in the constructor or `Initialize()` methods can cause noticeable stalls when loading a mod.

---

## ✨ 2. General Coding Best Practices

### A. Clean Code Principles
*   **Single Responsibility Principle (SRP):** Every class and method should have one, and only one, reason to change. Keep logic separated.
*   **Readability:** Use clear, descriptive naming conventions (e.g., `IsLoading` instead of `IL`). Adhere strictly to C# standards.
*   **Error Handling:** Utilize robust `try-catch` blocks, but be specific about exceptions (`try-catch(IOException)` vs. generic `catch (Exception)`). Always log failures gracefully using the provided logging infrastructure (`Logger`).

### B. Resource Management
*   **Disposal:** All resources that implement `IDisposable` must be wrapped in a `using` statement or explicitly disposed of to prevent memory leaks, which are particularly harmful on Android.
*   **Event Handling:** When subscribing to events (e.g., `+=`), agents *must* ensure they unsubscribe (`-=`) when the object is destroyed to prevent memory leaks and dangling references.

---

## ⚙️ 3. Fusion/BepInEx Specific Guidelines

### A. API Usage
*   **Configuration:** Use the provided `ConfigDefinition` system for all settings storage. Do not implement custom, manual file serialization logic; it must go through the defined API to ensure compatibility and proper type conversion (`TomlTypeConverter`).
*   **Logging:** Always use `Logger.LogMessage(...)` or the relevant specialized logger (e.g., `ConsoleManager`) instead of `Console.WriteLine()`. The logging system handles multiple sources and formats.

### B. Mod Lifecycle Awareness
1.  **Initialization Order:** Be acutely aware that code runs in a specific sequence: Preloader -> Core Loaders -> Plugins. Do not assume access to services or global states before the mod's `Initialize()` phase has successfully completed.
2.  **Dependency Injection:** If a service is required (e.g., an interface like `IService`), it must be accessed via the approved dependency resolution mechanism provided by Fusion, rather than hardcoding static references.

### C. Code Structure & Style
*   Keep related files together.
*   Use modern C# features (Pattern matching, records, etc.) where they improve safety or clarity, but ensure compatibility with the target .NET 10 version constraints.

---
***FAILURE TO FOLLOW THESE GUIDELINES WILL RESULT IN INSTABILITY OR PROJECT FAILURE ON ANDROID DEVICES. TREAT THIS DOCUMENT AS LAW.***