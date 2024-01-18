# Verwende das offizielle Quarkus-Build-Image als Basis
FROM quay.io/quarkus/centos-quarkus-maven:21.0.0-java11 AS build
WORKDIR /backend/src/main

# Kopiere den Anwendungscode
COPY . .

# Baue die Anwendung
RUN mvn clean package -DskipTests

# Extrahiere die erstellte JAR-Datei
RUN mkdir -p /usr/src/app/target/dependency && (cd target/dependency; jar -xf ../*.jar)

# Verwende ein leeres Image als Basis
FROM registry.access.redhat.com/ubi8/ubi-minimal:8.4

# Kopiere die extrahierte JAR-Datei aus dem Build-Image
COPY --from=build /usr/src/app/target/dependency/BOOT-INF/lib /deployments/lib/
COPY --from=build /usr/src/app/target/dependency/META-INF /deployments/META-INF/
COPY --from=build /usr/src/app/target/dependency/BOOT-INF/classes /deployments/

# Starte die Anwendung
CMD ["java", "-cp", "deployments/*", "io.quarkus.runner.GeneratedMain"]