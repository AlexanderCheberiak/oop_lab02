<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="html" encoding="UTF-8" doctype-public="-//W3C//DTD HTML 4.01 Transitional//EN" doctype-system="http://www.w3.org/TR/html4/loose.dtd"/>

	<xsl:template match="/">
		<html>
			<head>
				<title>Університет</title>
				<style>
					table {
					width: 100%;
					border-collapse: collapse;
					margin-bottom: 20px;
					}
					table, th, td {
					border: 1px solid black;
					}
					th, td {
					padding: 8px;
					text-align: left;
					}
					h1, h2, h3 {
					margin: 10px 0;
					}
				</style>
			</head>
			<body>
				<h1>Університет</h1>
				<!-- Групуємо дані по факультетах -->
				<xsl:for-each select="university/faculty">
					<h2>
						<xsl:value-of select="@name"/>
					</h2>
					<!-- Групуємо дані по кафедрах -->
					<xsl:for-each select="department">
						<h3>
							Кафедра: <xsl:value-of select="@name"/>
						</h3>
						<table>
							<thead>
								<tr>
									<th>Посада</th>
									<th>Ім'я</th>
									<th>Зарплата</th>
								</tr>
							</thead>
							<tbody>
								<!-- Для кожної кафедри виводимо співробітників -->
								<xsl:for-each select="employee">
									<tr>
										<td>
											<xsl:value-of select="position"/>
										</td>
										<td>
											<xsl:value-of select="name"/>
										</td>
										<td>
											<xsl:value-of select="salary"/>
										</td>
									</tr>
								</xsl:for-each>
							</tbody>
						</table>
					</xsl:for-each>
				</xsl:for-each>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
