USE mysql;

START TRANSACTION;

ALTER TABLE column_stats ADD COLUMN hist_type_1 enum('SINGLE_PREC_HB','DOUBLE_PREC_HB','JSON_HB') AFTER hist_type;

UPDATE column_stats SET hist_type_1 = hist_type;

ALTER TABLE column_stats DROP COLUMN hist_type;

ALTER TABLE column_stats RENAME COLUMN hist_type_1 TO hist_type;

ALTER TABLE column_stats ADD COLUMN histogram_1 LONGBLOB AFTER histogram;

UPDATE column_stats SET histogram_1 = histogram;

ALTER TABLE column_stats DROP COLUMN histogram;

ALTER TABLE column_stats RENAME COLUMN histogram_1 TO histogram;

COMMIT;


