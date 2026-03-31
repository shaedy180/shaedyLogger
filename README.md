# shaedy Logger

A CounterStrikeSharp plugin that logs all chat messages, connects, and disconnects to a local file.

## Features

- Logs global and team chat with timestamps
- Logs player connections and disconnections
- Async file I/O to avoid blocking the server
- Admin command to write custom log entries

## Commands

| Command | Description | Permission |
|---------|-------------|------------|
| `!svlog <message>` | Write a custom entry to the log | None |

## Installation

Drop the plugin folder into your CounterStrikeSharp `plugins` directory. Logs are written to `local_chat.log` in the plugin folder.

## Configuration

No config needed. Logging starts automatically.
