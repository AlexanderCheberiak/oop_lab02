<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" encoding="UTF-8" indent="yes" />

	<xsl:template match="/">
		<html>
			<head>
				<title>University Staff</title>
			</head>
			<body>
				<h1>University Staff Information</h1>
				<table border="1">
					<tr>
						<th>Name</th>
						<th>Faculty</th>
						<th>Position</th>
						<th>Salary</th>
					</tr>
					<xsl:for-each select="University/Person">
						<tr>
							<td>
								<xsl:value-of select="@Name" />
							</td>
							<td>
								<xsl:value-of select="@Faculty" />
							</td>
							<td>
								<xsl:value-of select="@Position" />
							</td>
							<td>
								<xsl:value-of select="@Salary" />
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
