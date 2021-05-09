package com.ipear.web.training;

import com.ipear.web.training.controller.AjaxController;
import org.junit.Assert;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

@RunWith(SpringRunner.class)
@SpringBootTest(classes = TrainingApplication.class)
@AutoConfigureMockMvc
class TrainingApplicationTests {

    private final TestRestTemplate restTemplate=new TestRestTemplate();

    @Autowired
    private MockMvc mvc;

    @Test
    void testGetProblemImage() {
        ArrayList images = restTemplate.getForObject("/getProblemImage/{1}", ArrayList.class, 33);
        for(int i=0;i<6;i++){
            Assertions.assertNotNull(images.get(i));
        }
    }

    @Test
    void testDoLogin() throws InterruptedException {
        // mvc.perform(get("").)
        Thread.sleep(1000*20);
    }

}
