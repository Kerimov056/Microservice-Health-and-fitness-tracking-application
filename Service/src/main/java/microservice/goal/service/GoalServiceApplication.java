package microservice.goal.service;

import org.springframework.amqp.rabbit.annotation.EnableRabbit;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;

@EntityScan(basePackages = {"com.goal"})
@ComponentScan({"com.goal"})
@EnableJpaRepositories(basePackages = {"com.goal"})
@EnableRabbit
@SpringBootApplication
public class GoalServiceApplication {

    public static void main(String[] args) {
        SpringApplication.run(GoalServiceApplication.class, args);
    }

}
