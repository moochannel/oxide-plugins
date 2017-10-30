# oxide-plugins

Oxide (ver.2) plugins for playing Rust on my server.

## Plugins

- PrioritySlot.cs - Inspired by [Reserved](http://oxidemod.org/plugins/reserved.674/), added using slot flexibly and auto-kick feature.

## Install

As same other Oxide plugins, place *[plugin_name]*.cs file into [oxide_dir]/plugins/ directory, then type ```oxide.load [plugin_name]``` on rcon console.

To change settings, modify [oxide_dir]/config/[plugin_name].json then type ```oxide.load [plugin_name]``` on rcon console.

To localize plugin, copy [oxide_dir]/lang/en/[plugin_name].json file to [oxide_dir]/lang/[your_lang_code]/ directory, modify json file, then type ```oxide.load [plugin_name]``` on rcon console.

## How to use

### PrioritySlot

Set a permission ```priorityslot.prior``` for players to use prior (reserved) slots.

By default, when prior players filled up prior slots, next prior player can use standard slots. Standard players cannot use prior slots. These behaviors can change by each slot.

When the server is full and prior player come, a standard player will be kicked. If the standard player already set a sleeping bag, the player will be teleported there before kicked.

### RefreshAnimals

On few players server, many animals will stay far from players, so players won't see animals in several hours.

This plugin removes animals are not moving--should be far from players--when a player login.
Removed animals will respawn in few minutes. Enjoy hunting!
