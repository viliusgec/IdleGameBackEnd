--
-- PostgreSQL database dump
--

-- Dumped from database version 14.0
-- Dumped by pg_dump version 14.2

-- Started on 2024-05-14 14:16:28

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5 (class 2615 OID 16394)
-- Name: Clicker; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA "Clicker";


ALTER SCHEMA "Clicker" OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 226 (class 1259 OID 24698)
-- Name: Achievements; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."Achievements" (
    "Id" integer NOT NULL,
    "Reward" integer,
    "Description" character varying,
    "TrainingName" character varying,
    "RequiredCount" integer
);


ALTER TABLE "Clicker"."Achievements" OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 24667)
-- Name: Battles; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."Battles" (
    "ID" integer NOT NULL,
    "Player" character varying,
    "Monster" character varying,
    "PlayerHP" integer,
    "MonsterHP" integer,
    "BattleFinished" boolean NOT NULL,
    "ItemGiven" boolean
);


ALTER TABLE "Clicker"."Battles" OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 24670)
-- Name: Battles_id_seq; Type: SEQUENCE; Schema: Clicker; Owner: postgres
--

ALTER TABLE "Clicker"."Battles" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "Clicker"."Battles_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 234 (class 1259 OID 24738)
-- Name: EquippedItems; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."EquippedItems" (
    "Id" integer NOT NULL,
    "PlayerUsername" character varying,
    "Item" character varying,
    "ItemPlace" character varying NOT NULL
);


ALTER TABLE "Clicker"."EquippedItems" OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 24722)
-- Name: IdleTrainings; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."IdleTrainings" (
    "Id" integer NOT NULL,
    "Name" character varying,
    "SkillName" character varying,
    "XpGiven" integer,
    "XpNeeded" integer
);


ALTER TABLE "Clicker"."IdleTrainings" OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 16405)
-- Name: Items; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."Items" (
    "Name" character varying NOT NULL,
    "Price" integer,
    "Level" integer,
    "Description" character varying,
    "isSellable" boolean DEFAULT false,
    "SellPrice" integer,
    "Type" character varying,
    "HP" integer,
    "Defense" integer,
    "Attack" integer
);


ALTER TABLE "Clicker"."Items" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 24636)
-- Name: MarketItems; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."MarketItems" (
    "Id" integer NOT NULL,
    "Amount" integer NOT NULL,
    "Price" integer NOT NULL,
    "ItemName" character varying NOT NULL,
    "Player" character varying NOT NULL
);


ALTER TABLE "Clicker"."MarketItems" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 24642)
-- Name: MarketItems_id_seq; Type: SEQUENCE; Schema: Clicker; Owner: postgres
--

ALTER TABLE "Clicker"."MarketItems" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "Clicker"."MarketItems_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 222 (class 1259 OID 24658)
-- Name: Monsters; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."Monsters" (
    "Name" character varying NOT NULL,
    "HP" integer NOT NULL,
    "Attack" integer NOT NULL,
    "Defense" integer NOT NULL,
    "DroppedItem" character varying,
    "ItemDropChance" integer,
    "XpGiven" integer,
    "Level" integer
);


ALTER TABLE "Clicker"."Monsters" OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 24704)
-- Name: PlayerAchievements; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."PlayerAchievements" (
    "Id" integer NOT NULL,
    "PlayerUsername" character varying NOT NULL,
    "AchievementId" integer NOT NULL,
    "Achieved" boolean DEFAULT false NOT NULL
);


ALTER TABLE "Clicker"."PlayerAchievements" OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 24716)
-- Name: PlayerIdleTrainings; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."PlayerIdleTrainings" (
    "Id" integer NOT NULL,
    "PlayerUsername" character varying,
    "IdleTrainingId" integer,
    "StartTime" timestamp with time zone,
    "Active" boolean
);


ALTER TABLE "Clicker"."PlayerIdleTrainings" OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16411)
-- Name: PlayerItems; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."PlayerItems" (
    "PlayerUsername" character varying NOT NULL,
    "ItemName" character varying NOT NULL,
    "Amount" integer,
    "IsEquiped" boolean,
    "Id" integer NOT NULL
);


ALTER TABLE "Clicker"."PlayerItems" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16438)
-- Name: PlayerItems_id_seq; Type: SEQUENCE; Schema: Clicker; Owner: postgres
--

ALTER TABLE "Clicker"."PlayerItems" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "Clicker"."PlayerItems_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 236 (class 1259 OID 32938)
-- Name: PlayerStatistics; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."PlayerStatistics" (
    "PlayerUsername" character varying NOT NULL,
    "TrainingName" character varying,
    "Count" integer,
    "Id" integer NOT NULL
);


ALTER TABLE "Clicker"."PlayerStatistics" OWNER TO postgres;

--
-- TOC entry 237 (class 1259 OID 32943)
-- Name: PlayerStatistics_id_seq; Type: SEQUENCE; Schema: Clicker; Owner: postgres
--

ALTER TABLE "Clicker"."PlayerStatistics" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "Clicker"."PlayerStatistics_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 212 (class 1259 OID 16398)
-- Name: Players; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."Players" (
    "InventorySpace" integer NOT NULL,
    "Money" integer NOT NULL,
    "IsInBattle" boolean NOT NULL,
    "Username" character varying NOT NULL,
    "IsInAction" boolean
);


ALTER TABLE "Clicker"."Players" OWNER TO postgres;

--
-- TOC entry 235 (class 1259 OID 32929)
-- Name: PvP; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."PvP" (
    "PlayerOne" character varying,
    "PlayerTwo" character varying,
    "Bet" integer,
    "Date" date,
    "Winner" character varying,
    "Id" integer NOT NULL
);


ALTER TABLE "Clicker"."PvP" OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 24648)
-- Name: ShopItems; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."ShopItems" (
    "ItemName" character varying NOT NULL
);


ALTER TABLE "Clicker"."ShopItems" OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 16408)
-- Name: Skills; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."Skills" (
    "Id" integer NOT NULL,
    "Name" character varying NOT NULL,
    "Experience" integer NOT NULL,
    "PlayerUsername" character varying NOT NULL
);


ALTER TABLE "Clicker"."Skills" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16414)
-- Name: Skills_id_seq; Type: SEQUENCE; Schema: Clicker; Owner: postgres
--

ALTER TABLE "Clicker"."Skills" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "Clicker"."Skills_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 217 (class 1259 OID 16433)
-- Name: TrainingSkills; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."TrainingSkills" (
    "SkillType" character varying NOT NULL,
    "SkillLevelRequired" integer NOT NULL,
    "XpGiven" integer NOT NULL,
    "GivenItem" character varying,
    "GivenItemAmount" integer,
    "NeededItem" character varying,
    "NeededItemAmount" integer,
    "TrainingName" character varying NOT NULL
);


ALTER TABLE "Clicker"."TrainingSkills" OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 16395)
-- Name: Users; Type: TABLE; Schema: Clicker; Owner: postgres
--

CREATE TABLE "Clicker"."Users" (
    "Username" character varying NOT NULL,
    "Email" character varying,
    "Password" character varying NOT NULL,
    "Role" character varying NOT NULL
);


ALTER TABLE "Clicker"."Users" OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 24697)
-- Name: achievements_id_seq; Type: SEQUENCE; Schema: Clicker; Owner: postgres
--

ALTER TABLE "Clicker"."Achievements" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "Clicker".achievements_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 233 (class 1259 OID 24737)
-- Name: equippeditems_id_seq; Type: SEQUENCE; Schema: Clicker; Owner: postgres
--

ALTER TABLE "Clicker"."EquippedItems" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "Clicker".equippeditems_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 231 (class 1259 OID 24721)
-- Name: idletrainings_id_seq; Type: SEQUENCE; Schema: Clicker; Owner: postgres
--

ALTER TABLE "Clicker"."IdleTrainings" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "Clicker".idletrainings_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 227 (class 1259 OID 24703)
-- Name: playerachievements_id_seq; Type: SEQUENCE; Schema: Clicker; Owner: postgres
--

ALTER TABLE "Clicker"."PlayerAchievements" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "Clicker".playerachievements_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 229 (class 1259 OID 24715)
-- Name: playeridletraining_id_seq; Type: SEQUENCE; Schema: Clicker; Owner: postgres
--

ALTER TABLE "Clicker"."PlayerIdleTrainings" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "Clicker".playeridletraining_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 3430 (class 0 OID 24698)
-- Dependencies: 226
-- Data for Name: Achievements; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."Achievements" ("Id", "Reward", "Description", "TrainingName", "RequiredCount") FROM stdin;
3	999	Willow trees chopped	Cut Willow	50
5	1000	Craft bronze sword	Craft Bronze Sword	1
6	1000	Mine copper	Mine Copper	1
7	1000	Smelt one bronze	Smelt Bronze	1
8	10000	Mine 100 copper	Mine Copper	100
9	10000	Smelt 100 bronze bars	Smelt Bronze	100
2	1000	Cut generic tree	Cut Tree	20
4	10000	Cook 1000 Tunas	Cut Tree	1000
1	1000	Cut your first tree	Cut Tree	1
\.


--
-- TOC entry 3427 (class 0 OID 24667)
-- Dependencies: 223
-- Data for Name: Battles; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."Battles" ("ID", "Player", "Monster", "PlayerHP", "MonsterHP", "BattleFinished", "ItemGiven") FROM stdin;
\.


--
-- TOC entry 3438 (class 0 OID 24738)
-- Dependencies: 234
-- Data for Name: EquippedItems; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."EquippedItems" ("Id", "PlayerUsername", "Item", "ItemPlace") FROM stdin;
\.


--
-- TOC entry 3436 (class 0 OID 24722)
-- Dependencies: 232
-- Data for Name: IdleTrainings; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."IdleTrainings" ("Id", "Name", "SkillName", "XpGiven", "XpNeeded") FROM stdin;
1	Cut Tree	Woodcutting	10	0
4	Mine Copper	Mining	10	0
5	Mine Iron	Mining	50	0
6	Fish Bass	Fishing	10	0
7	Burn Tree Log	Firemaking	10	0
8	Burn Oak Log	Firemaking	50	0
9	Cook Cod	Cooking	10	0
10	Smelt Bronze	Smithing	10	0
2	Cut Oak	Woodcutting	50	10
3	Cut Willow	Woodcutting	100	0
\.


--
-- TOC entry 3417 (class 0 OID 16405)
-- Dependencies: 213
-- Data for Name: Items; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."Items" ("Name", "Price", "Level", "Description", "isSellable", "SellPrice", "Type", "HP", "Defense", "Attack") FROM stdin;
copper ore	20	1	Copper ore	t	10	misc	\N	\N	\N
iron ore	50	1	Iron ore	t	25	misc	\N	\N	\N
gold ore	100	1	Gold ore	t	50	misc	\N	\N	\N
diamond	1000	1	Diamond	t	1000	misc	\N	\N	\N
oak log	50	1	Oak tree log	t	20	misc	\N	\N	\N
coal	50	1	Coal	t	30	misc	\N	\N	\N
bronze	50	1	Bronze bar	t	25	misc	\N	\N	\N
iron	100	1	Iron bar	t	50	misc	\N	\N	\N
gold	500	1	Gold bar	t	100	misc	\N	\N	\N
diamond sword	10000	40	Diamond sword	f	\N	tool	0	0	50
iron chestplate	2500	10	Iron chestplate	f	\N	chest	10	15	0
diamond chestplate	50000	40	Diamond chestplate	f	\N	chest	100	20	0
bronze sword	500	1	Bronze sword	f	\N	tool	0	0	20
bronze chestplate	1000	1	Bronze chestplate	f	\N	chest	0	5	0
raw cod	10	1	Raw cod	t	10	misc	\N	\N	\N
raw bass	100	1	Raw bass	t	100	misc	\N	\N	\N
raw salmon	1000	1	Raw salmon	t	1000	misc	\N	\N	\N
raw tuna	10000	1	Raw tuna	t	10000	misc	\N	\N	\N
cod	20	1	Cooked cod	t	20	misc	\N	\N	\N
bass	200	1	Cooked bass	t	200	misc	\N	\N	\N
salmon	2000	1	Cooked salmon	t	2000	misc	\N	\N	\N
tuna	20000	1	Cooked tuna	t	20000	misc	\N	\N	\N
iron sword	1000	40	Iron Sword	f	\N	tool	0	0	10
log	10	1	Tree Log	t	10	misc	\N	\N	\N
willow log	40	1	Willow tree log	t	50	misc	10	4	5
\.


--
-- TOC entry 3423 (class 0 OID 24636)
-- Dependencies: 219
-- Data for Name: MarketItems; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."MarketItems" ("Id", "Amount", "Price", "ItemName", "Player") FROM stdin;
\.


--
-- TOC entry 3426 (class 0 OID 24658)
-- Dependencies: 222
-- Data for Name: Monsters; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."Monsters" ("Name", "HP", "Attack", "Defense", "DroppedItem", "ItemDropChance", "XpGiven", "Level") FROM stdin;
Goblin	10	5	10	log	50	5	1
Strong Goblin	100	50	10	log	1	1	1
One Eyed Gabby	999	100	10	eyes	1	100	100
\.


--
-- TOC entry 3432 (class 0 OID 24704)
-- Dependencies: 228
-- Data for Name: PlayerAchievements; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."PlayerAchievements" ("Id", "PlayerUsername", "AchievementId", "Achieved") FROM stdin;
\.


--
-- TOC entry 3434 (class 0 OID 24716)
-- Dependencies: 230
-- Data for Name: PlayerIdleTrainings; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."PlayerIdleTrainings" ("Id", "PlayerUsername", "IdleTrainingId", "StartTime", "Active") FROM stdin;
\.


--
-- TOC entry 3419 (class 0 OID 16411)
-- Dependencies: 215
-- Data for Name: PlayerItems; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."PlayerItems" ("PlayerUsername", "ItemName", "Amount", "IsEquiped", "Id") FROM stdin;
\.


--
-- TOC entry 3440 (class 0 OID 32938)
-- Dependencies: 236
-- Data for Name: PlayerStatistics; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."PlayerStatistics" ("PlayerUsername", "TrainingName", "Count", "Id") FROM stdin;
\.


--
-- TOC entry 3416 (class 0 OID 16398)
-- Dependencies: 212
-- Data for Name: Players; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."Players" ("InventorySpace", "Money", "IsInBattle", "Username", "IsInAction") FROM stdin;
\.


--
-- TOC entry 3439 (class 0 OID 32929)
-- Dependencies: 235
-- Data for Name: PvP; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."PvP" ("PlayerOne", "PlayerTwo", "Bet", "Date", "Winner", "Id") FROM stdin;
\.


--
-- TOC entry 3425 (class 0 OID 24648)
-- Dependencies: 221
-- Data for Name: ShopItems; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."ShopItems" ("ItemName") FROM stdin;
log
oak log
iron ore
copper ore
willow log
bronze
\.


--
-- TOC entry 3418 (class 0 OID 16408)
-- Dependencies: 214
-- Data for Name: Skills; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."Skills" ("Id", "Name", "Experience", "PlayerUsername") FROM stdin;
\.


--
-- TOC entry 3421 (class 0 OID 16433)
-- Dependencies: 217
-- Data for Name: TrainingSkills; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."TrainingSkills" ("SkillType", "SkillLevelRequired", "XpGiven", "GivenItem", "GivenItemAmount", "NeededItem", "NeededItemAmount", "TrainingName") FROM stdin;
Woodcutting	5	20	oak log	2	\N	\N	Cut Oak
Firemaking	5	20	coal	1	oak log	1	Burn Oak log
Mining	20	150	gold ore	1	\N	\N	Mine Gold
Smithing	20	200	gold	1	gold ore	1	Smelt Gold
Fishing	0	40	raw cod	1	\N	\N	Fish Cod
Fishing	10	200	raw bass	1	\N	\N	Fish Bass
Fishing	20	1000	raw salmon	1	\N	\N	Fish Salmon
Fishing	40	5000	raw tuna	1	\N	\N	Fish Tuna
Woodcutting	0	10	log	1	\N	\N	Cut Tree
Firemaking	0	10	coal	1	log	1	Burn Tree log
Cooking	0	40	cod	1	raw cod	1	Cook Cod
Cooking	20	1000	salmon	1	raw salmon	1	Cook Salmon
Cooking	40	5000	tuna	1	raw tuna	1	Cook Tuna
Firemaking	30	50	coal	1	willog log	1	Burn Willow log
Woodcutting	40	100	oak log	1	\N	\N	Cut Dead tree
Woodcutting	25	50	willow log	1	\N	\N	Cut Willow
Cooking	10	200	bass	1	raw bass	1	Cook Bass
Smithing	0	50	bronze	1	copper ore	1	Smelt Bronze
Smithing	0	200	bronze sword	1	bronze	5	Craft Bronze sword
Smithing	0	500	bronze chestplate	1	bronze	10	Craft Bronze chestplate
Mining	0	30	copper ore	1	\N	\N	Mine Copper
Smithing	10	2000	iron chestplate	1	iron	10	Craft Iron chestplate
Smithing	10	500	iron	1	iron ore	1	Smelt Iron
Smithing	10	1000	iron sword	1	iron	5	Craft Iron sword
Mining	40	10000	diamond	1	\N	\N	Mine Diamond
Smithing	20	10000	diamond sword	1	diamond	5	Craft Diamond sword
Smithing	20	50000	diamond chestplate	1	diamond	10	Craft Diamond chestplate
Mining	5	500	iron ore	1	\N	\N	Mine Iron
\.


--
-- TOC entry 3415 (class 0 OID 16395)
-- Dependencies: 211
-- Data for Name: Users; Type: TABLE DATA; Schema: Clicker; Owner: postgres
--

COPY "Clicker"."Users" ("Username", "Email", "Password", "Role") FROM stdin;
\.


--
-- TOC entry 3447 (class 0 OID 0)
-- Dependencies: 224
-- Name: Battles_id_seq; Type: SEQUENCE SET; Schema: Clicker; Owner: postgres
--

SELECT pg_catalog.setval('"Clicker"."Battles_id_seq"', 116, true);


--
-- TOC entry 3448 (class 0 OID 0)
-- Dependencies: 220
-- Name: MarketItems_id_seq; Type: SEQUENCE SET; Schema: Clicker; Owner: postgres
--

SELECT pg_catalog.setval('"Clicker"."MarketItems_id_seq"', 30, true);


--
-- TOC entry 3449 (class 0 OID 0)
-- Dependencies: 218
-- Name: PlayerItems_id_seq; Type: SEQUENCE SET; Schema: Clicker; Owner: postgres
--

SELECT pg_catalog.setval('"Clicker"."PlayerItems_id_seq"', 49, true);


--
-- TOC entry 3450 (class 0 OID 0)
-- Dependencies: 237
-- Name: PlayerStatistics_id_seq; Type: SEQUENCE SET; Schema: Clicker; Owner: postgres
--

SELECT pg_catalog.setval('"Clicker"."PlayerStatistics_id_seq"', 6, true);


--
-- TOC entry 3451 (class 0 OID 0)
-- Dependencies: 216
-- Name: Skills_id_seq; Type: SEQUENCE SET; Schema: Clicker; Owner: postgres
--

SELECT pg_catalog.setval('"Clicker"."Skills_id_seq"', 99, true);


--
-- TOC entry 3452 (class 0 OID 0)
-- Dependencies: 225
-- Name: achievements_id_seq; Type: SEQUENCE SET; Schema: Clicker; Owner: postgres
--

SELECT pg_catalog.setval('"Clicker".achievements_id_seq', 9, true);


--
-- TOC entry 3453 (class 0 OID 0)
-- Dependencies: 233
-- Name: equippeditems_id_seq; Type: SEQUENCE SET; Schema: Clicker; Owner: postgres
--

SELECT pg_catalog.setval('"Clicker".equippeditems_id_seq', 6, true);


--
-- TOC entry 3454 (class 0 OID 0)
-- Dependencies: 231
-- Name: idletrainings_id_seq; Type: SEQUENCE SET; Schema: Clicker; Owner: postgres
--

SELECT pg_catalog.setval('"Clicker".idletrainings_id_seq', 10, true);


--
-- TOC entry 3455 (class 0 OID 0)
-- Dependencies: 227
-- Name: playerachievements_id_seq; Type: SEQUENCE SET; Schema: Clicker; Owner: postgres
--

SELECT pg_catalog.setval('"Clicker".playerachievements_id_seq', 52, true);


--
-- TOC entry 3456 (class 0 OID 0)
-- Dependencies: 229
-- Name: playeridletraining_id_seq; Type: SEQUENCE SET; Schema: Clicker; Owner: postgres
--

SELECT pg_catalog.setval('"Clicker".playeridletraining_id_seq', 5, true);


--
-- TOC entry 3263 (class 2606 OID 41125)
-- Name: Achievements achievements_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."Achievements"
    ADD CONSTRAINT achievements_pk PRIMARY KEY ("Id");


--
-- TOC entry 3261 (class 2606 OID 41153)
-- Name: Battles battles_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."Battles"
    ADD CONSTRAINT battles_pk PRIMARY KEY ("ID");


--
-- TOC entry 3271 (class 2606 OID 41133)
-- Name: EquippedItems equippeditems_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."EquippedItems"
    ADD CONSTRAINT equippeditems_pk PRIMARY KEY ("Id");


--
-- TOC entry 3269 (class 2606 OID 41123)
-- Name: IdleTrainings idletrainings_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."IdleTrainings"
    ADD CONSTRAINT idletrainings_pk PRIMARY KEY ("Id");


--
-- TOC entry 3246 (class 2606 OID 41135)
-- Name: Items items_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."Items"
    ADD CONSTRAINT items_pk PRIMARY KEY ("Name");


--
-- TOC entry 3255 (class 2606 OID 41137)
-- Name: MarketItems marketitems_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."MarketItems"
    ADD CONSTRAINT marketitems_pk PRIMARY KEY ("Id");


--
-- TOC entry 3259 (class 2606 OID 41155)
-- Name: Monsters monsters_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."Monsters"
    ADD CONSTRAINT monsters_pk PRIMARY KEY ("Name");


--
-- TOC entry 3265 (class 2606 OID 41127)
-- Name: PlayerAchievements playerachievements_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."PlayerAchievements"
    ADD CONSTRAINT playerachievements_pk PRIMARY KEY ("Id");


--
-- TOC entry 3267 (class 2606 OID 41157)
-- Name: PlayerIdleTrainings playeridletrainings_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."PlayerIdleTrainings"
    ADD CONSTRAINT playeridletrainings_pk PRIMARY KEY ("Id");


--
-- TOC entry 3250 (class 2606 OID 41139)
-- Name: PlayerItems playeritems_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."PlayerItems"
    ADD CONSTRAINT playeritems_pk PRIMARY KEY ("Id");


--
-- TOC entry 3244 (class 2606 OID 41145)
-- Name: Players players_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."Players"
    ADD CONSTRAINT players_pk PRIMARY KEY ("Username");


--
-- TOC entry 3275 (class 2606 OID 41141)
-- Name: PlayerStatistics playerstatistics_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."PlayerStatistics"
    ADD CONSTRAINT playerstatistics_pk PRIMARY KEY ("Id");


--
-- TOC entry 3273 (class 2606 OID 41143)
-- Name: PvP pvp_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."PvP"
    ADD CONSTRAINT pvp_pk PRIMARY KEY ("Id");


--
-- TOC entry 3257 (class 2606 OID 41129)
-- Name: ShopItems shopitems_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."ShopItems"
    ADD CONSTRAINT shopitems_pk PRIMARY KEY ("ItemName");


--
-- TOC entry 3248 (class 2606 OID 41151)
-- Name: Skills skills_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."Skills"
    ADD CONSTRAINT skills_pk PRIMARY KEY ("Id");


--
-- TOC entry 3252 (class 2606 OID 41131)
-- Name: TrainingSkills trainingskills_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."TrainingSkills"
    ADD CONSTRAINT trainingskills_pk PRIMARY KEY ("TrainingName");


--
-- TOC entry 3242 (class 2606 OID 41149)
-- Name: Users users_pk; Type: CONSTRAINT; Schema: Clicker; Owner: postgres
--

ALTER TABLE ONLY "Clicker"."Users"
    ADD CONSTRAINT users_pk PRIMARY KEY ("Username");


--
-- TOC entry 3253 (class 1259 OID 41121)
-- Name: trainingskills_trainingname_idx; Type: INDEX; Schema: Clicker; Owner: postgres
--

CREATE INDEX trainingskills_trainingname_idx ON "Clicker"."TrainingSkills" USING btree ("TrainingName");


-- Completed on 2024-05-14 14:16:29

--
-- PostgreSQL database dump complete
--

