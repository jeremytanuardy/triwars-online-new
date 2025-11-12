# MVP Scope - Minimum Viable Product

**Goal:** Create a playable prototype that demonstrates core 3-faction MMORPG gameplay.

**Timeline:** 6-9 months for MVP
**Team:** Solo dev + AI assistance (Claude)

---

## ✅ MVP MUST-HAVE Features

### 1. Character Creation & Selection

**IN SCOPE:**
- Select 1 of 3 races (Zenox, Terran, Sorcerion)
- Select 1 of 4 classes per race
- Basic character customization (preset faces/hairstyles)
- Name input + validation

**OUT OF SCOPE:**
- Advanced character creator
- Body type customization
- Multiple character slots (start with 1)

---

### 2. Core Movement & Controls

**IN SCOPE:**
- WASD movement
- Mouse camera control
- Jump
- Basic sprinting
- Auto-run toggle
- Tab-targeting system

**OUT OF SCOPE:**
- Mounts
- Swimming
- Flying
- Advanced parkour

---

### 3. Combat System (Simplified)

**IN SCOPE:**
- **4 skill hotkeys** per class (down from full skillset)
- Auto-attack (basic attack)
- HP/MP bars
- Target selection (Tab-targeting)
- Basic damage numbers
- Death & respawn at checkpoint

**OUT OF SCOPE:**
- Combo system
- Advanced skill trees
- Skill customization
- Crowd control effects (for MVP)

---

### 4. Stats & Progression

**IN SCOPE:**
- **Level 1-20** (down from 100)
- **4 core stats:** STR, DEX, INT, VIT
- Gain stat points on level up
- Manual stat distribution
- Experience bar
- Basic quest rewards

**OUT OF SCOPE:**
- Full level 1-100
- Skill point system
- Talent trees
- Class advancement

---

### 5. Equipment System (Basic)

**IN SCOPE:**
- **5 gear slots:** Weapon, Chest, Pants, Boots, Gloves
- Equipment stats (Attack, Defense, HP)
- Equip/unequip from inventory
- **Common + Uncommon gear only** (no rare/epic for MVP)
- Durability system (basic)

**OUT OF SCOPE:**
- Full gear slots (helm, accessories, etc.)
- Enhancement system (+1 to +20)
- Card system
- Ore system
- Transmog/cosmetics

---

### 6. Inventory & Items

**IN SCOPE:**
- **20-slot inventory** (simple)
- Stack items (up to 99)
- Use consumables (HP/MP potions)
- Drop/destroy items
- Vendor NPC (buy/sell)

**OUT OF SCOPE:**
- Bank/storage
- Auction house/marketplace
- Crafting system
- Quest items separate bag

---

### 7. NPCs & Quests

**IN SCOPE:**
- **5 basic kill quests** per starting zone
- Quest giver NPCs with dialogue
- Quest log (simple)
- Quest rewards (exp + gold + basic gear)
- Turn-in mechanics

**OUT OF SCOPE:**
- Complex quest chains
- Story/lore quests
- Daily quests
- Achievement system

---

### 8. World & Environment

**IN SCOPE:**
- **1 starting zone per faction** (3 zones total, small)
- **1 neutral town** (shared safe zone)
- **1 PvP battlefield map** (for testing faction war)
- Basic enemies (5-8 types total)
- Day/night cycle (simple)

**OUT OF SCOPE:**
- Multiple leveling zones
- Dungeons
- Instanced content
- Weather system
- Dynamic events

---

### 9. Faction System (Bare Minimum)

**IN SCOPE:**
- Assign player to faction based on race
- Faction name/color displayed
- Cannot attack same faction players
- Faction chat channel

**OUT OF SCOPE:**
- Faction ranks
- Faction quests
- Faction reputation
- Territory control
- Faction vendor

---

### 10. PvP & Faction War (PROTOTYPE)

**IN SCOPE:**
- **1 basic faction war map** (open field with 3 capture points)
- Manual testing only (no scheduling yet)
- Kill enemy factions = test combat
- Simple scoreboard
- Respawn at base

**OUT OF SCOPE:**
- Scheduled faction wars
- Automated matchmaking
- Rewards system
- Population balancing
- Base siege mechanics

---

### 11. Multiplayer (Basic)

**IN SCOPE:**
- See other players moving
- See other players attacking
- Chat system (global + party)
- Friend list (basic)
- Party system (max 4 players)

**OUT OF SCOPE:**
- Guild system
- Voice chat integration
- Advanced anti-cheat
- Server infrastructure (start local/LAN)

---

### 12. UI/UX

**IN SCOPE:**
- HP/MP/EXP bars
- Skill hotbar (4 skills)
- Inventory window
- Character stats window
- Quest log window
- Chat box
- Target frame

**OUT OF SCOPE:**
- Minimap
- World map
- Advanced HUD customization
- UI scaling options
- Multiple UI layouts

---

## 🔄 PHASED DEVELOPMENT APPROACH

### **Phase 1: Core Foundation (Months 1-2)**
- Unity project setup
- Character controller + camera
- Basic movement + jump
- Placeholder 3D models
- Simple terrain

### **Phase 2: Combat Prototype (Months 2-3)**
- Combat system (1 class first)
- HP/damage system
- 4 skills for 1 class
- Basic enemy AI
- Hit detection

### **Phase 3: Multiplayer Foundation (Months 3-4)**
- Mirror networking integration
- Player synchronization
- Basic chat
- Multiple players in world

### **Phase 4: Core Systems (Months 4-5)**
- Stats + leveling (1-20)
- Equipment system (5 slots)
- Inventory (20 slots)
- Vendor NPC

### **Phase 5: Content & Polish (Months 5-6)**
- 3 starting zones
- 5 quests per zone
- 12 classes implemented
- Basic UI polish

### **Phase 6: Faction War Prototype (Months 6-7)**
- 1 PvP map
- 3-faction conflict test
- Simple scoring
- Respawn system

### **Phase 7: Testing & Refinement (Months 7-9)**
- Playtesting with friends
- Bug fixing
- Balance adjustments
- Performance optimization

---

## 📦 MVP DELIVERABLE

**What success looks like:**

✅ Players can create character (3 races, 12 classes)
✅ Players can move, jump, attack in 3D world
✅ Players can complete 5 basic quests per faction zone
✅ Players can level 1-20 and distribute stats
✅ Players can equip gear from inventory
✅ Players can see and chat with other players
✅ Players can fight enemies (basic AI)
✅ Players can test 3-faction PvP combat on 1 map
✅ Game runs at 60 FPS with 10+ players in view

**Success Metrics:**
- 10+ hours of gameplay content
- 5 friends can play together without major bugs
- Core combat feels fun and responsive
- All 12 classes are playable (even if not perfectly balanced)

---

## ❌ EXPLICITLY OUT OF SCOPE

These will come AFTER MVP:

1. **Card System** - Post-MVP
2. **Ore Enhancement** - Post-MVP
3. **Guild System** - Post-MVP
4. **Marketplace/Auction House** - Post-MVP
5. **Dungeons** - Post-MVP
6. **Mounts** - Post-MVP
7. **Full Level 1-100** - MVP is 1-20
8. **Scheduled Faction Wars** - MVP is manual test only
9. **Monetization** - Post-MVP
10. **Advanced Graphics** - Keep it simple
11. **Cutscenes/Story** - Post-MVP
12. **Voice Acting** - Post-MVP

---

## 🎯 DEFINITION OF DONE

**MVP is complete when:**

1. ✅ Core gameplay loop works (quest → kill → level → gear)
2. ✅ All 12 classes are playable
3. ✅ Multiplayer works (5+ players simultaneously)
4. ✅ 3-faction PvP can be tested
5. ✅ No critical bugs (crashes, progression blockers)
6. ✅ Game is fun for at least 10 hours
7. ✅ Performance is acceptable (30+ FPS minimum)

---

## 📊 POST-MVP ROADMAP (High-Level)

**Version 0.2 - Enhancement Systems:**
- Card system implementation
- Ore enhancement (+1 to +20)
- Expanded level cap (20 → 40)

**Version 0.3 - Social Features:**
- Guild system
- Marketplace/Auction house
- More zones (level 20-40 content)

**Version 0.4 - Faction Wars:**
- Scheduled faction wars
- Automated matchmaking
- Rewards system
- Territory control

**Version 0.5 - Endgame Content:**
- Dungeons
- World bosses
- Level cap 40 → 60

**Version 1.0 - Launch:**
- Polish everything
- Level cap to 100
- Monetization (cosmetics)
- Marketing push

---

## 🛠️ TECH STACK (MVP)

**Engine:** Unity 2022 LTS
**Networking:** Mirror (free, proven for MMO)
**Language:** C#
**Version Control:** Git + GitHub
**Assets:** Free/cheap Unity Asset Store + AI-generated
**Database:** SQLite (local) → PostgreSQL (later)
**Backend:** None for MVP (peer-to-peer/listen server)

---

## 💰 MVP BUDGET ESTIMATE

**Assuming solo dev + AI assistance:**

- **Unity License:** Free (Personal)
- **Assets:** $0-200 (free assets + cheap packs)
- **Hosting:** $0 (local/LAN testing)
- **Tools:** $0 (VS Code, Git, Blender)
- **Total:** $0-200

**Time Investment:** 6-9 months @ 20-30 hours/week = 480-1080 hours

---

**Next Steps:**
1. Review and approve this MVP scope
2. Create detailed technical architecture (see [TDD](tdd.md))
3. Set up development environment
4. Begin Phase 1: Core Foundation

**Questions to answer before starting:**
- Is 1-20 level cap acceptable for MVP? (recommend YES)
- Should we cut to 6 classes (2 per race) for MVP? (recommend NO, keep 12)
- Are we okay with placeholder graphics? (recommend YES)
- Local/LAN multiplayer vs dedicated server for MVP? (recommend local first)
