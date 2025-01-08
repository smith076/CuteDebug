# CuteDebug Class Documentation

A static utility class that provides enhanced debugging capabilities in Unity, with author-specific debug filtering and conditional compilation support.

## Properties

### CurrentUser
- **Type**: `string`
- **Description**: Gets the current user name from Unity Editor preferences or CloudProjectSettings.
- **Returns**: The current user name in Editor mode, empty string in runtime.
- **Platform**: Only functional in Unity Editor.

## Core Methods

### ShouldShowDebug
```csharp
private static bool ShouldShowDebug(string authorName, [CallerMemberName] string memberName = "", 
    [CallerFilePath] string sourceFilePath = "")
```
- **Description**: Determines whether debug messages should be displayed based on the author name.
- **Parameters**:
  - `authorName`: The name of the author to filter debug messages.
  - `memberName`: Automatically captured calling member name.
  - `sourceFilePath`: Automatically captured source file path.
- **Returns**: `true` if debug messages should be shown, `false` otherwise.
- **Behavior**: 
  - Checks for CuteDebugAttribute if no author name is provided
  - Always returns `false` in builds
  - Shows debug messages only if authorName matches CurrentUser or is empty

## Logging Methods

### Log (string message overload)
```csharp
public static void Log(string message, GameObject gameObject = null, string authorName = null)
```
- **Description**: Logs a message to the Unity console with optional GameObject context.
- **Parameters**:
  - `message`: The message to log
  - `gameObject`: Optional GameObject for context in console
  - `authorName`: Optional author name for filtering
- **Conditional Compilation**: Only active when DEBUG_SYMBOL is defined
- **Format**: `[AuthorName] message` if author provided, else just `message`

### Log (GameObject overload)
```csharp
public static void Log(GameObject gameObject, string authorName = null)
```
- **Description**: Logs a GameObject's name to the Unity console.
- **Parameters**:
  - `gameObject`: The GameObject to log
  - `authorName`: Optional author name for filtering
- **Conditional Compilation**: Only active when DEBUG_SYMBOL is defined
- **Format**: `[AuthorName] gameObject.name` if author provided, else just `gameObject.name`

### Log (List<Object> overload)
```csharp
public static void Log(List<Object> ts, string authorName = null)
```
- **Description**: Logs each Object in a list to the Unity console.
- **Parameters**:
  - `ts`: List of Objects to log
  - `authorName`: Optional author name for filtering
- **Conditional Compilation**: Only active when DEBUG_SYMBOL is defined
- **Format**: `[AuthorName] object.ToString()` for each object if author provided

### Warning
```csharp
public static void Warning(string message, string authorName = null)
```
- **Description**: Logs a warning message to the Unity console.
- **Parameters**:
  - `message`: The warning message to log
  - `authorName`: Optional author name for filtering
- **Conditional Compilation**: Only active when DEBUG_SYMBOL is defined
- **Format**: `[AuthorName] message` if author provided, else just `message`

### Error
```csharp
public static void Error(string message, string authorName = null)
```
- **Description**: Logs an error message to the Unity console.
- **Parameters**:
  - `message`: The error message to log
  - `authorName`: Optional author name for filtering
- **Conditional Compilation**: Only active when DEBUG_SYMBOL is defined
- **Format**: `[AuthorName] message` if author provided, else just `message`

### Assert
```csharp
public static void Assert(bool condition, string message = "", string authorName = null)
```
- **Description**: Performs a conditional assert with an optional message.
- **Parameters**:
  - `condition`: The condition to check
  - `message`: Optional message to display if assertion fails
  - `authorName`: Optional author name for filtering
- **Conditional Compilation**: Only active when DEBUG_SYMBOL is defined
- **Format**: `[AuthorName] message` if author provided, else just `message`

## CuteDebugAttribute Class

### Properties
- **AuthorName**: Gets the name of the author associated with the decorated method.

### Constructor
```csharp
public CuteDebugAttribute(string authorName)
```
- **Description**: Creates a new instance of CuteDebugAttribute.
- **Parameters**:
  - `authorName`: The name of the author to associate with the decorated method.
- **Usage**: Apply to methods to automatically provide author context for debug messages.

## Usage Notes

1. Debug messages are only shown in the Unity Editor, not in builds.
2. Messages can be filtered by author name, showing only messages from the current user.
3. The DEBUG_SYMBOL must be defined for any debug functionality to work.
4. Author names can be provided either directly in method calls or via the CuteDebugAttribute.
