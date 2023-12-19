ALTER TABLE "Rooms"  ADD COLUMN temp_room_number integer;

UPDATE "Rooms"  SET temp_room_number = CAST("RoomNumber"  AS integer);

ALTER TABLE "Rooms"  DROP COLUMN "RoomNumber";

ALTER TABLE "Rooms"  RENAME COLUMN "temp_room_number" TO "RoomNumber";