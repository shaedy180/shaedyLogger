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

## Support

If you find a bug, have a feature request, or something isn't working as expected, feel free to [open an issue](../../issues). I'll take a look when I can.

Custom plugins are available on request, potentially for a small fee depending on scope. Reach out via an issue or at access@shaedy.de.

> Note: These repos may not always be super active since most of my work happens in private repositories. But issues and requests are still welcome.

## Donate

If you want to support my work: [ko-fi.com/shaedy](https://ko-fi.com/shaedy)
