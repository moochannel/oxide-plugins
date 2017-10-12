# oxide-plugins

Oxide (ver.2) plugins for playing Rust on my server.

## Plugins

- PrioritySlot.cs - Inspired by [Reserved](http://oxidemod.org/plugins/reserved.674/), added using slot flexibly and auto-kick feature.

## Install

As same other Oxide plugins, place *[plugin_name]*.cs file into [oxide_dir]/plugins/ directory, then type ```oxide.load [plugin_name]``` on rcon console.

To change settings, modify [oxide_dir]/config/[plugin_name].json then type ```oxide.load [plugin_name]``` on rcon console.

To localize plugin, copy [oxide_dir]/lang/en/[plugin_name].json file to [oxide_dir]/lang/[your_lang_code]/ directory, modify json file, then type ```oxide.load [plugin_name]``` on rcon console.
