package com.goal.config;

import org.springframework.amqp.core.Binding;
import org.springframework.amqp.core.BindingBuilder;
import org.springframework.amqp.core.FanoutExchange;
import org.springframework.amqp.core.Queue;
import org.springframework.amqp.rabbit.connection.ConnectionFactory;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.amqp.support.converter.MessageConverter;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class RabbitConfig {

    public static final String USER_CREATED_QUEUE = "user-created-queue";
    public static final String USER_CREATED_EXCHANGE = "Contracts.Events:UserCreatedEvent";

    @Bean
    public FanoutExchange userCreatedExchange() {
        return new FanoutExchange(USER_CREATED_EXCHANGE);
    }

    @Bean
    public Queue userCreatedQueue() {
        return new Queue(USER_CREATED_QUEUE);
    }

    @Bean
    public Binding bindingUserCreated(Queue userCreatedQueue, FanoutExchange userCreatedExchange) {
        return BindingBuilder.bind(userCreatedQueue).to(userCreatedExchange);
    }

    @Bean
    public MessageConverter jsonMessageConverter() {
        return new CustomJackson2JsonMessageConverter();
    }

    @Bean
    public RabbitTemplate rabbitTemplate(ConnectionFactory connectionFactory) {
        RabbitTemplate template = new RabbitTemplate(connectionFactory);
        template.setMessageConverter(jsonMessageConverter());
        return template;
    }
}
