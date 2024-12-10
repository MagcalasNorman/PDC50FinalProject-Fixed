<?php
include 'db.php';

$sql = "SELECT DISTINCT GradeLvl FROM tblstudent";
$result = $conn->query($sql);

$gradeLevels = [];
while ($row = $result->fetch_assoc()) {
    $gradeLevels[] = $row['GradeLvl'];
}

echo json_encode($gradeLevels);
$conn->close();
?>