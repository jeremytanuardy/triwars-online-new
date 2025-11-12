# Core Game Systems

## Card System (RF Online Inspired)

### Overview
The Card System allows players to socket special cards into equipment for bonus stats and abilities. This is a core progression system inspired by RF Online.

### Card Types

#### 1. **Stat Cards**
Add flat stat bonuses to equipment:
- **STR Card:** +Strength
- **DEX Card:** +Dexterity
- **INT Card:** +Intelligence
- **VIT Card:** +Vitality
- **All Stat Card:** +All stats (rare)

#### 2. **Element Cards**
Add elemental damage/resistance:
- **Fire Card:** +Fire damage or resist
- **Ice Card:** +Ice damage or resist
- **Lightning Card:** +Lightning damage or resist
- **Dark Card:** +Dark damage or resist

#### 3. **Special Effect Cards**
Unique bonuses:
- **Critical Card:** +Critical rate
- **Speed Card:** +Movement/attack speed
- **HP Drain Card:** Lifesteal effect
- **Defense Penetration Card:** Ignore % of defense

### Card Rarity
- **Common** (white): +1-5 stat
- **Uncommon** (green): +6-10 stat
- **Rare** (blue): +11-20 stat
- **Epic** (purple): +21-35 stat
- **Legendary** (orange): +36-50 stat + special effect

### Card Slots
- **Weapon:** 1-3 slots (based on tier)
- **Armor:** 1-2 slots per piece
- **Accessories:** 1 slot

### Obtaining Cards
1. **Monster Drops** - Low % chance
2. **Dungeon Bosses** - Higher % chance
3. **Faction War Rewards** - Rare cards
4. **Card Crafting** - Combine lower tier cards
5. **Marketplace** - Buy from players

### Card Removal
- **Safe Removal:** Pay gold, keep card
- Cards are tradeable after removal
- Can swap cards anytime out of combat

---

## Ore Enhancement System

### Overview
Ore system allows weapons to gain elemental properties and scaling damage. Complementary to Card system.

### Ore Types

#### Basic Ores (Tier 1-3):
- **Copper Ore:** +1-5 damage
- **Iron Ore:** +6-15 damage
- **Steel Ore:** +16-30 damage

#### Elemental Ores (Tier 4-5):
- **Flame Ore:** +Fire element + DoT
- **Frost Ore:** +Ice element + slow
- **Storm Ore:** +Lightning element + chain
- **Shadow Ore:** +Dark element + blind

#### Rare Ores (Tier 6):
- **Mithril Ore:** High bonus + no element
- **Adamantite Ore:** Highest bonus + crit
- **Orichalcum Ore:** Legendary tier

### Enhancement Process

1. **Select Weapon** to enhance
2. **Select Ore** type
3. **Enhancement Level** +1 to +20
   - **+0 to +9:** 100% success (safe zone)
   - **+10 to +15:** 70% success
   - **+16 to +20:** 50% success
   - **Failure:** Enhancement level drops by -1 (NOT destroyed!)

### Enhancement Benefits
Each +1 enhancement:
- +2% base weapon damage
- +1 to relevant stat (STR/DEX/INT)
- Visual glow effect (higher = brighter)

At key milestones:
- **+5:** Minor glow effect
- **+10:** Moderate glow + particle effects
- **+15:** Strong glow + enhanced particles
- **+20:** Legendary glow + special aura

### Success Rate Modifiers
- **Blessing Scroll:** +10% success rate (cash shop - convenience, not P2W)
- **Guild Buff:** +5% success rate
- **Event Bonus:** +10% during events
- **Max Stack:** +25% total

### Ore Gathering
1. **Mining Nodes** in world
2. **Dungeon Ore Veins** (better ores)
3. **Faction War Mines** (best ores)
4. **Daily Login Rewards**
5. **Marketplace Trading**

---

## Combat System

### Stats Overview

#### Primary Stats:
- **STR (Strength):** Melee physical damage
- **DEX (Dexterity):** Ranged physical damage, critical rate
- **INT (Intelligence):** Magic damage, skill power
- **VIT (Vitality):** HP, physical defense

#### Secondary Stats (Derived):
- **HP:** VIT × 10 + base
- **Energy/MP:** INT × 5 + base
- **Physical Attack:** STR or DEX + weapon damage
- **Magic Attack:** INT + weapon magic
- **Physical Defense:** VIT + armor value
- **Magic Defense:** INT × 0.5 + armor magic defense
- **Critical Rate:** DEX × 0.1% + bonuses
- **Critical Damage:** 150% base + bonuses
- **Attack Speed:** DEX affects cooldowns
- **Movement Speed:** Base + bonuses

### Damage Formula

**Physical Damage:**
```
Base Damage = (Physical Attack - Enemy Defense × 0.7)
Final Damage = Base Damage × Skill Multiplier × (1 + Bonuses)
Critical = Final Damage × Critical Multiplier
```

**Magic Damage:**
```
Base Damage = (Magic Attack - Enemy Magic Defense × 0.6)
Final Damage = Base Damage × Skill Multiplier × (1 + Bonuses)
```

**Elemental Damage:**
```
If weakness: ×1.5 damage
If resistance: ×0.5 damage
If immune: 0 damage
```

### Combat Mechanics

#### Targeting System:
- **Tab-targeting** (traditional MMO)
- **Soft lock-on** available
- **AoE skills** hit multiple targets in radius

#### Cooldown System:
- **Global Cooldown (GCD):** 1 second
- **Skill-specific Cooldowns:** 3-60 seconds
- **Ultimate Abilities:** 120-300 seconds

#### Resource System:
- **Energy/MP** for skills
- **Regenerates** out of combat (10% per second)
- **In-combat regen:** 2% per second + bonuses

#### Status Effects:
- **Stun:** Cannot act (1-3 seconds)
- **Slow:** Movement speed -50%
- **Silence:** Cannot use skills
- **DoT:** Damage over time (poison, burn, bleed)
- **Buffs:** Temporary stat increases
- **Debuffs:** Temporary stat decreases

---

## Faction War System

### Schedule
- **2 times per week**
- **Duration:** 2 hours each
- **Times:** Announced 48 hours in advance
- **Rotation:** Different days each week to accommodate timezones

### Objectives

#### 1. **Mine Control**
- 3 mines on battlefield
- Hold mine = generate faction points
- Points convert to rewards

#### 2. **Base Siege**
- Attack enemy faction bases
- Destroy core = massive point bonus
- Defensive structures can be built

#### 3. **Elimination Points**
- Kill enemy players = points
- Assist = half points
- Bonus for kill streaks

### Rewards

**Victory Rewards:**
- Rare equipment boxes
- Faction currency (large amount)
- Exclusive cosmetics
- Territory bonuses for next week

**Participation Rewards:**
- Based on contribution points
- Everyone who participates gets something
- Scaled by individual performance

### Balance Mechanics

#### Population Balancing:
- **Faction over 40% server pop:** Cannot create new characters
- **Faction under 25% server pop:** +20% stat buff during war
- **Underdog Bonus:** Outnumbered faction gets damage/defense buff

#### Territory System:
- **Controlled Territory:** Passive bonuses
- **Mining Speed:** +10% in faction territory
- **Experience:** +5% in faction territory
- **Special Vendors:** Access to exclusive items

---

## Death & Respawn

### PvE Death:
- **Respawn:** At nearest checkpoint or town
- **Death Penalty:** -2% durability on all equipment
- **NO:** Gear loss, exp loss, or currency loss

### PvP/Faction War Death:
- **Respawn:** At faction base (30 second timer)
- **Death Penalty:** -1 contribution point (faction war only)
- **NO:** Gear loss ever

### Durability System:
- **Equipment Durability:** Degrades with use and death
- **Repair Cost:** Gold-based, reasonable
- **0 Durability:** Stats reduced to 0 (not destroyed)
- **Repair NPC:** Available in every town

---

## Progression Curve

### Leveling Speed:
- **Level 1-30:** 10-15 hours (Tutorial + early game)
- **Level 30-60:** 20-30 hours (Mid game)
- **Level 60-80:** 30-40 hours (Late game)
- **Level 80-100:** 40-50 hours (Endgame)
- **Total:** ~100-135 hours to max level (casual pace)

### Gear Progression:
- **Common Gear:** Vendor + early drops
- **Uncommon:** Quest rewards + dungeons
- **Rare:** Dungeon bosses + crafting
- **Epic:** Raid bosses + faction wars
- **Legendary:** World bosses + hardest content

### Enhancement Curve:
- **+0 to +5:** Common, easy to achieve
- **+5 to +10:** Expected for endgame casual
- **+10 to +15:** Hardcore player goal
- **+15 to +20:** Top 1% prestige items

---

See also:
- [Overview](overview.md) - Core game concept
- [Classes](classes/) - Class-specific abilities
- [MVP Scope](../02-technical/mvp-scope.md) - What launches first
