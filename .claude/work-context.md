# Konteks Kerja - Triwars Online

**Last Updated:** 2025-11-12 15:30 (sebelum restart session)

---

## 🎯 Struktur Tim

- **Jeremy (User):** Decision maker, Game Designer, Otak project
- **Claude (AI):** Technical executor, Coding, Documentation
- **Status:** Tim 2 orang (nanti hire orang lagi)

**PENTING:** Jeremy = Otak, Claude = Tangan. Jangan bikin keputusan design sendiri. Kalo ragu, TANYA.

---

## 📋 Aturan Kerja

### 1. Komunikasi
- **Bahasa:** SELALU Bahasa Indonesia (MUTLAK)
- **Tone:** Serius, profesional, NO lawakan (user stress kalo AI sok asik)
- **Format:** Langsung to the point, no bullshit

### 2. Work Flow
- User = kasih keputusan & arah
- Claude = execute & implement
- Selalu konfirmasi dulu sebelum keputusan besar
- Catat progress setiap session

### 3. Documentation
- **GDD Location:** `/docs` (Markdown + Git)
- **Format:** Markdown files (.md)
- **Version Control:** Git + GitHub
- **Repo:** jeremytanuardy/triwars-online-new
- **Notion:** DEPRECATED - ga dipake lagi (migration complete)

### 4. Progress Tracking
- **Tool:** Todo list (TodoWrite) untuk short-term tasks
- **Context:** File ini untuk long-term progress
- **Serena MCP:** Sekarang AKTIF untuk persistent memory
- **Update:** Secara berkala dengan timestamp

---

## 📊 Project Overview

**Nama:** Triwars Online
**Genre:** 3-Faction MMORPG
**Inspiration:** RF Online (modernized)
**Engine:** Unity 2022 LTS
**Networking:** Mirror
**Timeline MVP:** 6-9 bulan

### Factions
1. **Zenox** - Tech Warriors (4 classes)
2. **Terran** - Balanced Humans (4 classes)
3. **Sorcerion** - Mystic Power (4 classes)

**Total:** 12 classes

---

## ✅ Progress Log

### 2025-11-12 Session 1 - Complete Documentation Migration
**Duration:** Full session
**Status:** COMPLETED ✅

#### Completed Tasks:
- [x] Migrasi dari Notion ke Markdown + Git (100% complete)
- [x] Setup folder structure `/docs` dengan 4 kategori
- [x] Buat `.gitignore` untuk Unity project
- [x] Dokumentasi lengkap 12 classes (3 races × 4 classes each):
  - Zenox: Sentinel, Energy Manipulator, Scout, Siege Engineer
  - Terran: Blade Specialist, Marksman, Technomancer, MAU Pilot
  - Sorcerion: Berserker, Shadow Ranger, Warlock, Inferno Summoner
- [x] Game overview lengkap (core concept, monetization, systems)
- [x] Core systems doc (Card, Ore, Combat, Faction War)
- [x] MVP scope detail (6-9 bulan, level 1-20, phased approach)
- [x] Setup konteks kerja di `.claude/` folder:
  - `.claude/work-context.md` (file ini)
  - `.claude/instructions.md` (aturan kerja permanent)
- [x] Setup Serena MCP untuk persistent memory
- [x] 3 commits pushed ke GitHub:
  - `36e61f2` - Initial docs structure
  - `2803f64` - Complete game design docs (1,232 lines)
  - `6c67508` - Claude work context (298 lines)

#### Key Learnings:
1. **Notion Issues:** Notion MCP terbatas, susah organize, ga cocok untuk GDD
2. **Markdown Advantage:** Gua bisa langsung edit files, version control lebih baik
3. **Race vs Classes:** 3 RACES (Zenox, Terran, Sorcerion), bukan 6 classes. Total 12 classes.
4. **Communication:** User prefer Bahasa Indonesia, serius, no lawakan

#### Git Commits Detail:
```
36e61f2 - Setup docs structure + README
2803f64 - Add complete game design documentation
6c67508 - Add Claude work context and instructions
```

**Next Steps (PENDING):**
- [ ] Review MVP scope dengan Jeremy (tunggu approval)
- [ ] Setup Unity project (after approval)
- [ ] Mulai Phase 1: Core Foundation (character controller + movement)
- [ ] Tentukan asset sources (free/paid?)
- [ ] Tentukan multiplayer approach untuk MVP (local/LAN vs dedicated)

---

## 🔄 Next Update Schedule

**Planned:** Setelah review MVP scope + setup Unity project
**Reminder:** Update file ini setiap major milestone atau setiap minggu (whichever comes first)

---

## 🚀 SETELAH RESTART SESSION - BACA INI DULU

**Context Status:** ✅ Full documentation migration DONE

**What was completed:**
- Semua dokumentasi game design udah di `/docs` (Markdown format)
- Konteks kerja udah di `.claude/` folder
- Serena MCP udah setup (connected)
- Git repo active dengan 3 commits

**What to do NEXT (tunggu instruksi Jeremy):**
1. Review MVP scope di `docs/02-technical/mvp-scope.md`
2. Kalo approved → Setup Unity project structure
3. Kalo ada revisi → Update docs sesuai feedback

**JANGAN:**
- Jangan langsung bikin keputusan sendiri
- Jangan assume apa yang user mau
- Jangan ngelawak atau sok asik
- Jangan lupa pake Bahasa Indonesia

**Current Phase:** Planning selesai, waiting for approval to start development
**Current Branch:** `main`
**Latest Commit:** `6c67508` (Claude work context)

---

## 📝 Notes & Decisions

### Key Decisions Made:
1. **Documentation:** Pindah dari Notion ke Markdown+Git (approved 2025-11-12)
   - Reason: Notion MCP buggy, ga bisa organize properly, Markdown better for version control
2. **MVP Scope:** Level 1-20, 4 skills per class, basic systems only
3. **Classes:** Keep all 12 classes for MVP (not reducing to 6)
4. **Graphics:** Placeholder assets OK for MVP
5. **Communication:** Bahasa Indonesia ONLY, no lawakan, serious tone
6. **Context Tracking:** Serena MCP + manual `.claude/work-context.md`

### Pending Decisions:
- Unity project structure setup (next priority)
- Asset sources (free/paid?)
- Multiplayer: Local/LAN vs dedicated server untuk MVP
- Character creator scope untuk MVP

---

## 📚 Documentation Structure (COMPLETED)

```
docs/
├── README.md (Master index)
├── 01-game-design/
│   ├── overview.md ✅ (Game concept, pillars, monetization)
│   ├── races.md ✅ (3 factions overview)
│   ├── core-systems.md ✅ (Card, Ore, Combat, Faction War)
│   └── classes/
│       ├── zenox.md ✅ (4 classes)
│       ├── terran.md ✅ (4 classes)
│       └── sorcerion.md ✅ (4 classes)
├── 02-technical/
│   └── mvp-scope.md ✅ (6-9 month plan, phased approach)
├── 03-business/ (empty, future)
└── 04-creative/ (empty, future)

.claude/
├── work-context.md ✅ (File ini - progress tracking)
└── instructions.md ✅ (Aturan kerja permanent)
```

**Total Documentation:** ~1,530 lines across 10+ files
**Status:** Migration from Notion COMPLETE ✅

---

## 🛠️ Tech Stack

**Engine:** Unity 2022 LTS
**Language:** C#
**Networking:** Mirror
**Version Control:** Git + GitHub
**Database (future):** SQLite → PostgreSQL

**MCP Tools (Active):**
- ✅ **serena-audit** - Persistent memory & context tracking (BARU SETUP)
- ✅ **UnityMCP** - Unity project integration (auto-detected)
- ✅ **playwright** - Browser automation (available)
- ✅ **notionApi** - Notion integration (deprecated, ga kepake)

**Command untuk Serena:**
```bash
uvx --from git+https://github.com/oraios/serena serena-mcp-server
```

**Catatan:** Serena baru disetup, mungkin perlu restart session biar tools fully available.

---

**Format update:**
```
### YYYY-MM-DD - [Milestone Name]
- [x] Completed task 1
- [x] Completed task 2
- [ ] Pending task 1

**Decisions:**
- Decision made dengan reasoning

**Next:**
- Next action items
```
