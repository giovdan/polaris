TYPE=TRIGGERS
triggers='CREATE DEFINER=`root`@`localhost` TRIGGER IF NOT EXISTS `trg_attributevalue_before_update` \nBEFORE UPDATE ON `attributevalue`\nFOR EACH ROW SET NEW.RowVersion = OLD.RowVersion + 1'
sql_modes=524288
definers='root@localhost'
client_cs_names='utf8mb4'
connection_cl_names='utf8mb4_general_ci'
db_cl_names='latin1_swedish_ci'
created=167534157302
