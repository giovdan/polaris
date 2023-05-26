USE Machine;

START TRANSACTION;

UPDATE applicationsetting SET DefaultValue = 999 WHERE `code` = 'DbVersion';

COMMIT;