-- CreateTable
CREATE TABLE "CustomRole" (
    "userSnowflake" TEXT NOT NULL,
    "guildSnowflake" TEXT NOT NULL,
    "roleSnowflake" TEXT NOT NULL
);

-- CreateIndex
CREATE UNIQUE INDEX "CustomRole_userSnowflake_guildSnowflake_key" ON "CustomRole"("userSnowflake", "guildSnowflake");
